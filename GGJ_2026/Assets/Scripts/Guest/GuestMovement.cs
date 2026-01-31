using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GuestMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GuestController gc;

    [Header("Movement variables")]
    [SerializeField] private float guestSpeed;
    [SerializeField] private float tileProximityNeeded;

    [Header("Events")]
    [SerializeField] private UnityEvent onMovementEnd;

    private Vector2Int finalDestinationPos = Vector2Int.zero;
    private Vector2Int tileDestinationPos = Vector2Int.zero;
    private Vector2Int tileDestinationVector = Vector2Int.zero;

    private Vector2Int currentTilePos;

    private bool xStrideBigger;

    private void FixedUpdate()
    {
        if (gc.currentAction == GuestActions.Moving)
        {
            MoveToDestination();
        }
    }

    public void MoveToRandomTile()
    {
        MoveToTile(MapController.instance.GetRandomTile());
    }

    public void MoveToTile(Vector2Int tile)
    {
        finalDestinationPos = MapController.instance.GetTilePos(tile.x, tile.y);

        Vector2Int moveDestinationVector = finalDestinationPos - currentTilePos;
        xStrideBigger = Mathf.Abs(moveDestinationVector.x) >= Mathf.Abs(moveDestinationVector.y);

        gc.ChangeCurrentState(GuestActions.Moving);

        SetTileDestination();
    }

    private void SetTileDestination()
    {
        tileDestinationVector = CalculateTileDestination();
        tileDestinationPos = MapController.instance.PosToTile(transform.position) + tileDestinationVector;

        if (tileDestinationVector == Vector2Int.zero)
        {
            EndMovement();
        }
    }

    private Vector2Int CalculateTileDestination()
    {
        Vector2Int tileDestinationVector = Vector2Int.zero;

        currentTilePos = MapController.instance.PosToTile(transform.position);
        Vector2Int moveDestinationVector = finalDestinationPos - currentTilePos;

        if (xStrideBigger)
        {
            tileDestinationVector = CalculateXTileMovement(moveDestinationVector);

            if (tileDestinationVector != Vector2Int.zero)
                return tileDestinationVector;
            else
                return CalculateYTileMovement(moveDestinationVector);
        }

        tileDestinationVector = CalculateYTileMovement(moveDestinationVector);

        if (tileDestinationVector != Vector2Int.zero)
            return tileDestinationVector;
        else
            return CalculateXTileMovement(moveDestinationVector);
    }

    private Vector2Int CalculateXTileMovement(Vector2Int moveVector)
    {
        if (moveVector.x > 0)
        {
            return Vector2Int.right;
        }
        if (moveVector.x < 0)
        {
            return Vector2Int.left;
        }
        return Vector2Int.zero;
    }

    private Vector2Int CalculateYTileMovement(Vector2Int moveVector)
    {
        if (moveVector.y > 0)
        {
            return Vector2Int.up;
        }
        if (moveVector.y < 0)
        {
            return Vector2Int.down;
        }
        return Vector2Int.zero;
    }

    private void MoveToDestination()
    {
        Vector3 positionDiff = (Vector3)(guestSpeed * Time.deltaTime * (Vector2)tileDestinationVector);

        transform.position += positionDiff;

        Vector2Int newTilePos = MapController.instance.PosToTile(transform.position);

        if (Vector2.Distance(transform.position, tileDestinationPos) < tileProximityNeeded)
        {
            if (newTilePos.x != currentTilePos.x)
            {
                if (newTilePos.x == finalDestinationPos.x)
                {
                    transform.position = new Vector3(finalDestinationPos.x, transform.position.y, 0);
                }
            }
            else if (newTilePos.y != currentTilePos.y)
            {
                if (newTilePos.y == finalDestinationPos.y)
                {
                    transform.position = new Vector3(transform.position.x, finalDestinationPos.y, 0);
                }
            }

            SetTileDestination();
        }
    }

    private void EndMovement() 
    {
        onMovementEnd.Invoke();
    }
}
