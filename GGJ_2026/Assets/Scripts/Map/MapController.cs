using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MapController : MonoBehaviour
{
    public static MapController instance { get; private set; }

    [Header("References")]
    [SerializeField] private MapGrid mg;
    [SerializeField] private MapInteractions mi;
    [SerializeField] private MapTraits mt;

    [Header("Map Variables")]
    public GuestController[] guests;
    public GuestController mrMascara;

    private void Awake()
    {
        if (instance != null) 
        {
            Destroy(instance);
        }
        instance = this;

        mg.CalculateGridSize();
        mi.Initialize();
        mt.SetGuestsTraits(guests);
    }

    #region Wrappers

    public Vector2Int GetRandomTile()
    {
        return mg.GetRandomTile();
    }

    public Vector2Int GetRandomTilePos()
    {
        return mg.GetRandomTilePos();
    }

    public Vector2Int GetTilePos(int x, int y)
    {
        return mg.GetTilePos(x, y);
    }

    public Vector2Int RoundVector(Vector2 position)
    {
        return mg.RoundVector(position);
    }

    public Vector2Int PosToTile(Vector2 position)
    {
        return mg.PosToTile(position);
    }

    public List<InteractionObject> GetInteractionObjects()
    {
        return mi.GetInteractionObjects();
    }

    #endregion
}
