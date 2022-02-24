using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameBuilder : MonoBehaviour
{
    public int RC_COUNT = 7;

    public Sprite beeIcon;
    public Sprite waspIcon;

    public Player player1 = null;
    public Player player2 = null;

    #region Singleton

    public static GameBuilder instance = null;
    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    #endregion

    public void StartSinglePlayer()
    {
        if(player1 != null)
        {
            DestroyImmediate(player1);
            DestroyImmediate(player2);
        }

        player1 = new GameObject().AddComponent<HumanPlayer>();
        player1.transform.parent = this.transform;
        player1.name = "PLAYER";
        player1.icon = beeIcon;
        player1.playType = Play.Horizontal;

        player2 = new GameObject().AddComponent<IA_Player>();
        player2.transform.parent = this.transform;
        player2.name = "IA_PLAYER";
        player2.icon = waspIcon;
        player2.playType = Play.Vertical;

        //DontDestroyOnLoad(player1);
        //DontDestroyOnLoad(player2);

        SceneManager.LoadScene("Game");
    }

    public void StartTwoPlayers()
    {
        if (player1 != null)
        {
            DestroyImmediate(player1);
            DestroyImmediate(player2);
        }

        player1 = new GameObject().AddComponent<HumanPlayer>();
        player1.transform.parent = this.transform;
        player1.name = "PLAYER 1";
        player1.icon = beeIcon;
        player1.playType = Play.Horizontal;

        player2 = new GameObject().AddComponent<HumanPlayer>();
        player2.transform.parent = this.transform;
        player2.name = "PLAYER 2";
        player2.icon = waspIcon;
        player2.playType = Play.Vertical;

        //DontDestroyOnLoad(player1);
        //DontDestroyOnLoad(player2);

        SceneManager.LoadScene("Game");
    }
}
