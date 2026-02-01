using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueHandler : MonoBehaviour
{
    public static DialogueHandler Instance { get; private set; }

    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject sprite;
    [SerializeField] private TextMeshProUGUI dialogue;
    [SerializeField] private TextMeshProUGUI charName;


    [SerializeField] private DialogueBox[] dialogues;
    private int index = 0;

    public UnityEvent onDialogueEnd;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    void Start()
    {
        Sprite newSprite = dialogues[index].char_sprite;
        sprite.GetComponent<Image>().sprite = newSprite;
        dialogue.text = dialogues[index].text;
        charName.text = dialogues[index].char_name;
    }

    public void NextDialogueInput(InputAction.CallbackContext context) 
    {
        if (context.performed)
            NextDialogue();
    }

    public void DialogueStart(string chosenName, Sprite chosenSprite, string[] chosenDialogues)
    {
        for (int i = 0; i < dialogues.Length; i++)
        {
            dialogues[i].char_name = chosenName;
            dialogues[i].char_sprite = chosenSprite;
            dialogues[i].text = chosenDialogues[i];
        }
        Sprite newSprite = dialogues[index].char_sprite;
        sprite.GetComponent<Image>().sprite = newSprite;
        dialogue.text = dialogues[index].text;
        charName.text = dialogues[index].char_name;
        GameManager.Instance.GetPlayerController().SetPlayerState(PlayerState.Blocked);
        canvas.SetActive(true);
    }

    public void NextDialogue()
    {
        print(dialogues);
        index++;
        if (index < dialogues.Length)
        {
            dialogue.text = dialogues[index].text;
            charName.text = dialogues[index].char_name;
        }
        else
        {
            index = 0;
            DialogueEnd();
        }
    }

    private void DialogueEnd()
    {
        canvas.SetActive(false);
        GameManager.Instance.GetPlayerController().SetPlayerState(PlayerState.Idle);
    }
}

