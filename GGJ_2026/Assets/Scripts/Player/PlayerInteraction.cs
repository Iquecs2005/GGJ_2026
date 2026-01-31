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
    [SerializeField] private LayerMask hitMask;

    public void MoveInteractionBubble() 
    {
        interactionCenter.localPosition = pc.facingDirection;
    }

    public void Interact() 
    {
        GameObject interactableObject = GetInteractableObject();
        if (interactableObject != null)
        {
            IPlayerInteractable interactionScript = interactableObject.GetComponent<IPlayerInteractable>();    
            interactionScript.Interact();
        }
    }

    private GameObject GetInteractableObject() 
    {
        RaycastHit2D[] hitResults = Physics2D.CircleCastAll(interactionCenter.position, interactionRadius, Vector2.zero, 0, hitMask);

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
