using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnManager : MonoBehaviour
{

    public List<Player> players = new List<Player>();
    private Player currentPlayer;
    private int currentIndex = 0;
    public Player CurrentPlayer { get { return currentPlayer;} }
    public int CurrentIndex { get { return currentIndex; } }

    #region Singleton

    public static TurnManager instance;
    public void Awake()
    {
        instance = this;

        if (GameBuilder.instance != null)
        {
            players.Clear();
            players.Add(GameBuilder.instance.player1);
            players.Add(GameBuilder.instance.player2);
        }
    }

    #endregion

    public void Initialize()
    {
        foreach(var pl in players) pl.Load();
        currentPlayer = players[currentIndex];
    }
    public void NextTurn()
    {
        currentIndex++;
        if(currentIndex >= players.Count)
        {
            currentIndex = 0;
        }
        currentPlayer = players[currentIndex];

    }

}
