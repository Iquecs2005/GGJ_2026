using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class GuestController : MonoBehaviour
{
    [SerializeField] private float timeBetweenActions;

    [SerializeField] private float guestSpeed;
    [SerializeField] private float tileProximityNeeded;

    private enum GuestActions
    {
        Ready, Moving, Waiting, Blocked
    }
    private GuestActions currentAction = GuestActions.Ready;

    private Vector2Int finalDestinationPos = Vector2Int.zero;
    private Vector2Int tileDestinationPos = Vector2Int.zero;
    private Vector2Int tileDestinationVector = Vector2Int.zero;

    private Vector2Int currentTilePos;

    private void Start()
    {
        //StartCoroutine(PlayerTurnDelay());
    }

    private void FixedUpdate()
    {
        switch (currentAction) 
        {
            case GuestActions.Ready:
                SetDestinationToRandomTile();
                break;
            case GuestActions.Moving:
                MoveToDestination();
                break;
        }
    }

    private IEnumerator PlayerTurnDelay() 
    {
        yield return new WaitForSeconds(timeBetweenActions);
        currentAction = GuestActions.Ready;
    }

    private void SetDestinationToRandomTile() 
    {
        SetFinalDestination(MapController.instance.GetRandomTile());
    }

    private void SetFinalDestination(Vector2Int destination) 
    {
        finalDestinationPos = destination;
        currentAction = GuestActions.Moving;
        if (SetTileDestination()) 
        {
            currentAction = GuestActions.Waiting;
            StartCoroutine(PlayerTurnDelay());
        }
    }

    private bool SetTileDestination() 
    {
        tileDestinationVector = CalculateTileDestination();
        tileDestinationPos = MapController.instance.GetTilePos(transform.position) + tileDestinationVector;
        if (tileDestinationVector == Vector2Int.zero)
        {
            return true;
        }
        return false;
    }

    private Vector2Int CalculateTileDestination() 
    {
        currentTilePos = MapController.instance.GetTilePos(transform.position);
        Vector2Int moveDestinationVector = finalDestinationPos - currentTilePos;

        if (moveDestinationVector.x != 0)
        {
            if (moveDestinationVector.x > 0)
            {
                return Vector2Int.right;
            }
            else
            {
                return Vector2Int.left;
            }
        }
        if (moveDestinationVector.y != 0)
        {
            if (moveDestinationVector.y > 0)
            {
                return Vector2Int.up;
            }
            else
            {
                return Vector2Int.down;
            }
        }

        return Vector2Int.zero;
    }

    private void MoveToDestination() 
    {
        Vector3 positionDiff = (Vector3)(guestSpeed * Time.deltaTime * (Vector2)tileDestinationVector);

        transform.position += positionDiff;

        Vector2Int newTilePos = MapController.instance.GetTilePos(transform.position);

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

            if (SetTileDestination())
            {
                currentAction = GuestActions.Waiting;
                StartCoroutine(PlayerTurnDelay());
            }

        }
    }
}
