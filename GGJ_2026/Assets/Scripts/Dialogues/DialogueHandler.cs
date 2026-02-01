using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            NextDialogue();
        }
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
        canvas.SetActive(true);
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

    private void DialogueEnd()
    {
        canvas.SetActive(false);
    }
}

