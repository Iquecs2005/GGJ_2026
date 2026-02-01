using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    [SerializeField] private string activityName;
    [SerializeField] private float activityTime;
    [SerializeField] private Transform interactionTransform;

    public Vector2Int interactionTile { get; private set; }
    
    private bool occupied;
    private GuestController currentGuest;

    public void Initialize() 
    {
        interactionTile = MapController.instance.PosToTile(interactionTransform.position);
        MapController.instance.AddUnwalkableSpot(interactionTile);
    } 

    public string GetName() 
    {
        return activityName;
    }

    public bool IsOccupied() 
    {
        return occupied;
    }

    public bool SetOccupied(GuestController guest) 
    {
        if (IsOccupied())
            return false;

        occupied = true;
        currentGuest = guest;

        return true;
    }

    public bool Activate(GuestController guest) 
    {
        if (guest != currentGuest) 
            return false;

        StartCoroutine(InteractionTimer());

        return true;
    }

    private IEnumerator InteractionTimer() 
    {
        yield return new WaitForSeconds(activityTime);
        currentGuest.OnActivityEnd();
        occupied = false;
    }
}
