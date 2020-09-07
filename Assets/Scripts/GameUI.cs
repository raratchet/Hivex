using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Image currentIcon;
    public Text winText;

    #region Singleton

    public static GameUI instance;
    public void Awake()
    {
        instance = this;
    }

    #endregion

    public void Start()
    {
        winText.gameObject.SetActive(false);
    }

    public void Update()
    {
        currentIcon.sprite = TurnManager.instance.CurrentPlayer.icon;
    }

    public void ShowWinText(string text)
    {
        winText.text = "El ganador es " + text;
        winText.gameObject.SetActive(true);
    }

}
