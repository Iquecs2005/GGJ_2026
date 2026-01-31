using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestController : MonoBehaviour
{
    [Header("References")]
    public GuestMovement gm;
    public GuestAI gai;

    [HideInInspector] public GuestActions currentAction = GuestActions.Ready;

    public void ChangeCurrentState(GuestActions newAction) 
    {
        currentAction = newAction;
    }
}

public enum GuestActions
{
    Ready, Moving, Waiting, Blocked
}