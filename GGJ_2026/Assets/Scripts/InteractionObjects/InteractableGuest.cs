using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableGuest : MonoBehaviour, IPlayerInteractable
{
    public void Interact() 
    {
        print("Começou dialogo com " + gameObject.name);
    }
}
