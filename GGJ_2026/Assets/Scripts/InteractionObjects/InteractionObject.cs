using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionObject : MonoBehaviour
{
    [SerializeField] private string activityName;
    [SerializeField] private Transform interactionTransform;

    public Vector2Int interactionTile { get; private set; }

    public void Initialize() 
    {
        interactionTile = MapController.instance.PosToTile(interactionTransform.position);
    } 

    public string GetName() 
    {
        return activityName;
    }
}
