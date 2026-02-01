using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestDialogues : MonoBehaviour
{
    [SerializeField] private string[] uniqueDialogues;

    public DialogueBox[] chosenDialogues;

    void Start()
    {
        chosenDialogues[0].text = uniqueDialogues[Random.Range(0, uniqueDialogues.Length)];
        chosenDialogues[1].text = "teste"; //sortear depois texto da fala da dica
    }
}
