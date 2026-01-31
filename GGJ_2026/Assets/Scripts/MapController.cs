using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    public static MapController instance { get; private set; } 

    [SerializeField] private Transform bottomWall;
    [SerializeField] private Transform topWall;
    [SerializeField] private Transform leftWall;
    [SerializeField] private Transform rightWall;

    public Vector2Int gridSize { get; private set; }
    private Vector2Int initialTilePos;

    private void Awake()
    {
        if (instance != null) 
        {
            Destroy(instance);
        }
        instance = this;

        CalculateGridSize();
    }

    private void CalculateGridSize() 
    {
        int xCount = (int)(rightWall.position.x - leftWall.position.x);
        int yCount = (int)(topWall.position.y - bottomWall.position.y);

        gridSize = new Vector2Int(xCount, yCount);
        initialTilePos = new Vector2Int((int)leftWall.position.x, (int)bottomWall.position.y);
    }

    public Vector2Int GetRandomTile()
    {
        Vector2Int randomTile = Vector2Int.zero;

        randomTile.x = Random.Range(0, gridSize.x);
        randomTile.y = Random.Range(0, gridSize.y);

        return randomTile;
    }

    public Vector2Int GetRandomTilePos() 
    {
        Vector2Int randomTile = GetRandomTile();

        return GetTilePos(randomTile.x, randomTile.y);
    }

    public Vector2Int GetTilePos(int x, int y) 
    {
        Vector2Int tilePos = Vector2Int.zero;

        tilePos.x = x;
        tilePos.y = y;

        tilePos += initialTilePos;

        return tilePos;
    }

    public Vector2Int PosToTile(Vector2 position)
    {
        Vector2Int tilePos = Vector2Int.zero;

        tilePos.x = Mathf.RoundToInt(position.x);
        tilePos.y = Mathf.RoundToInt(position.y);

        return tilePos;
    }
}
