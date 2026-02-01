using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    public static HintManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    public string[] introvertHints;
    public string[] extrovertHints;
    public string[] meatHints;
    public string[] veggieHints;
    public string[] drinkHints;
    public string[] noDrinkHints;
}
