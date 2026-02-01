using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerController pc;

    [Header("Interaction Variables")]
    [SerializeField] private Transform interactionCenter;
    [SerializeField] private float interactionRadius;
    [SerializeField] private LayerMask interactMask;
    [SerializeField] private LayerMask blameMask;

    private bool notebookOpen = false;

    public void MoveInteractionBubble() 
    {
        interactionCenter.localPosition = pc.facingDirection;
    }

    public void Interact() 
    {
        if (pc.playerState == PlayerState.Blocked)
        {
            return;
        }

        GameObject interactableObject = GetClosestObject(interactMask);
        if (interactableObject != null)
        {
            IPlayerInteractable interactionScript = interactableObject.GetComponent<IPlayerInteractable>();    
            interactionScript.Interact();
        }
    }

    public void Blame() 
    {
        if (pc.playerState == PlayerState.Blocked)
        {
            return;
        }

        GameObject guestObject = GetClosestObject(blameMask);
        if (guestObject != null)
        {
            GuestController guestController = guestObject.GetComponent<GuestController>();
            BlameManager.instance.ActivateBlameConfirm(guestController);
            pc.SetPlayerState(PlayerState.Blocked);
        }
    }

    public void Notebook()
    {
        if (pc.playerState == PlayerState.Blocked)
        {
            return;
        }

        if (!notebookOpen)
        {
            NotebookManager.Instance.OpenNotebook();
        }
        else
        {
            NotebookManager.Instance.CloseNotebook();
        }
        notebookOpen = !notebookOpen;
    }

    private GameObject GetClosestObject(LayerMask layerMask) 
    {
        RaycastHit2D[] hitResults = Physics2D.CircleCastAll(interactionCenter.position, interactionRadius, Vector2.zero, 0, layerMask);

        if (hitResults.Length == 0)
            return null;

        int minimumIndex = 0;
        float minimumDistance = Vector2.Distance(interactionCenter.position, hitResults[0].transform.position);

        for (int i = 1; i < hitResults.Length; i++)
        {
            float currentDistance = Vector2.Distance(interactionCenter.position, hitResults[i].transform.position);
            if (currentDistance < minimumDistance)
            {
                minimumDistance = currentDistance;
                minimumIndex = i;
            }
        }

        return hitResults[minimumIndex].transform.gameObject;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(interactionCenter.position, interactionRadius);
    }
}
