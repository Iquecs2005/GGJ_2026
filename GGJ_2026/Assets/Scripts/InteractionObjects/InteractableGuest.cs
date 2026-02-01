using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableGuest : MonoBehaviour, IPlayerInteractable
{
    public void Interact() 
    {
        GuestDialogues guestDialogues = gameObject.GetComponent<GuestDialogues>();
        if (guestDialogues != null)
        {
            string name = guestDialogues.chosenDialogues[0].char_name;
            Sprite sprite = guestDialogues.chosenDialogues[0].char_sprite;
            List<string> dialogues = new List<string>();
            for (int i = 0; i < guestDialogues.chosenDialogues.Length; i++)
            {
                dialogues.Add(guestDialogues.chosenDialogues[i].text);
            }
            DialogueHandler.Instance.DialogueStart(name, sprite, dialogues.ToArray());
        }
        //print("Começou dialogo com " + gameObject.name);
    }
}
