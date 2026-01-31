using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInteractions : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MapController mc;

    [Header("Interaction Variables")]
    [SerializeField] private List<InteractionObject> interactionObjects;

    public void Initialize()
    {
        foreach (InteractionObject interaction in interactionObjects)
        {
            interaction.Initialize();
        }
    }

    public List<InteractionObject> GetInteractionObjects()
    {
        List<InteractionObject> interactionsCopy = new List<InteractionObject>(interactionObjects);

        return interactionsCopy;
    }
}
