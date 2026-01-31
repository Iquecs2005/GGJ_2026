using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTraits : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MapController mc;

    [Header("Trait Variables")]
    [SerializeField] private PersonalityTrait[] availableTraits;

    public void SetGuestsTraits(GuestController[] guestList) 
    {
        foreach (GuestController guest in mc.guests)
        {
            TraitDecision(guest);
        }
    }

    private void TraitDecision(GuestController guest) 
    {
        List<PersonalityTrait> guestTraits = new List<PersonalityTrait>();

        foreach (PersonalityTrait trait in availableTraits)
        {
            float chance = UnityEngine.Random.Range(0, 100);

            if (chance < trait.chance)
                guestTraits.Add(trait);
        }

        guest.SetTraits(guestTraits);
    }
}

[Serializable]
public class PersonalityTrait 
{
    public string name;
    public float chance;
    public InteractionObject[] impossibleInteractions;
}
