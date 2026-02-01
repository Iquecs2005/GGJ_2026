using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestTraitManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GuestController gc;

    public List<PersonalityTrait> personalityTraits;

    public void SetTraits(List<PersonalityTrait> newTraits) 
    {
        personalityTraits = newTraits;
        gc.onTraitsModification.Invoke();
    }

    public List<PersonalityTrait> GetTraits() 
    {
        List<PersonalityTrait> traitListCopy = new List<PersonalityTrait>(personalityTraits);

        return traitListCopy;
    }

    public void RemoveConflictingInteractions(ref List<InteractionObject> interactionObjects) 
    {
        foreach (PersonalityTrait trait in personalityTraits)
        {
            foreach (InteractionObject conflictingInteraction in trait.impossibleInteractions) 
            {
                interactionObjects.Remove(conflictingInteraction);
            }
        }
    }
}
