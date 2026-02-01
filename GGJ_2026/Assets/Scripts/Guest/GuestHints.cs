using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestHints : MonoBehaviour
{
    public string hintChosen;

    private void Start()
    {
        List<PersonalityTrait> traits = MapController.instance.mrMascara.GetTraits();
        List<string> traitNames = new List<string>();
        for (int i = 0; i < traits.Count; i++) 
        {
            traitNames.Add(traits[i].name);
        }

        int hintType = Random.Range(0, 3);
        if (hintType == 0)
        {
            if (traitNames.Contains("Introvert"))
            {
                hintChosen = HintManager.Instance.introvertHints[Random.Range(0, HintManager.Instance.introvertHints.Length)];
            }
            else
            {
                hintChosen = HintManager.Instance.extrovertHints[Random.Range(0, HintManager.Instance.extrovertHints.Length)];
            }
        }
        else if (hintType == 1)
        {
            if (traitNames.Contains("Vegetarian"))
            {
                hintChosen = HintManager.Instance.veggieHints[Random.Range(0, HintManager.Instance.veggieHints.Length)];
            }
            else
            {
                hintChosen = HintManager.Instance.meatHints[Random.Range(0, HintManager.Instance.meatHints.Length)];
            }
        }
        else
        {
            if (traitNames.Contains("Non-Alcohol"))
            {
                hintChosen = HintManager.Instance.noDrinkHints[Random.Range(0, HintManager.Instance.noDrinkHints.Length)];
            }
            else
            {
                hintChosen = HintManager.Instance.drinkHints[Random.Range(0, HintManager.Instance.drinkHints.Length)];
            }
        }

    }
}
