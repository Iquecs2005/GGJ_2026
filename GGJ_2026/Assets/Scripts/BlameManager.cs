using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class BlameManager : MonoBehaviour
{
    public static BlameManager instance { get; private set; }

    [SerializeField] private GameObject UIHolder;
    [SerializeField] private TMP_Text accusedText;

    private GuestController blamedGuest;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;
    }

    public void ActivateBlameConfirm(GuestController guest) 
    {
        Time.timeScale = 0;

        blamedGuest = guest;
        accusedText.text = blamedGuest.GetName();
        UIHolder.SetActive(true);

        GameManager.Instance.GetPlayerController().SetPlayerState(PlayerState.Blocked);
    }

    public void ConfirmBlame() 
    {
        Time.timeScale = 1;

        if (MapController.instance.mrMascara == blamedGuest)
            SceneManager.LoadScene("WinScreen");
        else
            SceneManager.LoadScene("LoseScreen");

        UIHolder.SetActive(false);
    }

    public void CancelBlame()
    {
        Time.timeScale = 1;

        UIHolder.SetActive(false);
        GameManager.Instance.GetPlayerController().SetPlayerState(PlayerState.Idle);
    }
}
