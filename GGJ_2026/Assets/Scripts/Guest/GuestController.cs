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
    [SerializeField] private string guestName;
    [SerializeField] private Vector2Int facingDir;    

    [Header("Events")]
    public UnityEvent onActivityEnd;
    public UnityEvent onMovementEnd;
    public UnityEvent onNearMovementEnd;
    public UnityEvent onTraitsModification;
    public UnityEvent<Vector2Int> onFacingDirChange;

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

    public string GetName() 
    {
        return guestName;
    }

    public void SetFacingDir(Vector2Int newDir) 
    {
        facingDir = newDir;
        onFacingDirChange.Invoke(facingDir);
    }
}

public enum GuestActions
{
    Ready, Moving, Waiting, Blocked, Acting
}