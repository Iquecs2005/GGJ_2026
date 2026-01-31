using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GuestController : MonoBehaviour
{
    [Header("References")]
    public GuestMovement gm;
    public GuestAI gai;
    public GuestTraitManager gtm;

    [Header("General Variables")]
    public GuestActions currentAction = GuestActions.Ready;

    [Header("Events")]
    public UnityEvent onActivityEnd;
    public UnityEvent onMovementEnd;
    public UnityEvent onNearMovementEnd;
    public UnityEvent onTraitsModification;

    public void ChangeCurrentState(GuestActions newAction) 
    {
        currentAction = newAction;
    }

    public void OnActivityEnd() 
    {
        onActivityEnd.Invoke();
    }

    public void SetTraits(List<PersonalityTrait> newTraits) 
    {
        gtm.SetTraits(newTraits);
    }
}

public enum GuestActions
{
    Ready, Moving, Waiting, Blocked, Acting
}