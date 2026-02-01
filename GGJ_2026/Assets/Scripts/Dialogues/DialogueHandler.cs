using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour
{
    [SerializeField] private GameObject sprite;
    [SerializeField] private TextMeshProUGUI dialogue;
    [SerializeField] private TextMeshProUGUI charName;


    [SerializeField] private DialogueBox[] dialogues;
    private int index = 0;


    void Start()
    {
        Sprite newSprite = dialogues[index].char_sprite;
        sprite.GetComponent<Image>().sprite = newSprite;
        dialogue.text = dialogues[index].text;
        charName.text = dialogues[index].char_name;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            NextDialogue();
        }
    }

    public void DialogueStart()
    {
        
    }

    public void NextDialogue()
    {
        if (index < dialogues.Length)
        {
            dialogue.text = dialogues[index].text;
            charName.text = dialogues[index].char_name;
            ++index;
        }
        else
        {
            index = 0;
            DialogueEnd();
        }
    }

    public void DialogueEnd()
    {

    }
}

