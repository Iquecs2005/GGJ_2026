using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookManager : MonoBehaviour
{
    public static NotebookManager Instance { get; private set; }

    [SerializeField] private GameObject canvas;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        Instance = this;
    }

    public void OpenNotebook()
    {
        canvas.SetActive(true);
    }

    public void CloseNotebook()
    {
        canvas.SetActive(false);
    }
}
