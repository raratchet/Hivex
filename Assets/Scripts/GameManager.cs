using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    #region Singleton

    public static GameManager instance;
    public void Awake()
    {
        instance = this;
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        TurnManager.instance.Initialize();
    }

    void TileSelection(Tile selectedTile)
    {
        CheckForWin(selectedTile);
        TurnManager.instance.NextTurn();
    }

    void CheckForWin(Tile selected)
    {
        if (CheckForPath(selected,TurnManager.instance.CurrentPlayer.playType))
        {
            Debug.Log("Hay conexion para " + TurnManager.instance.CurrentPlayer);
            EndGame();
        }
    }

    void EndGame()
    {
        //Mostrar el ganador
        GameUI.instance.ShowWinText(TurnManager.instance.CurrentPlayer.name);
        //Volver al menu despues de x segundos
        Invoke("BackToMenu", 2);
    }

    void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    bool CheckForPath(Tile current, Play playType)
    {
        List<Tile> conectedTiles = new List<Tile>();
        Player owner = TurnManager.instance.CurrentPlayer;
        CheckForConec(current, owner, ref conectedTiles);
        bool flag1 = false;
        bool flag2 = false;

        foreach(Tile tile in conectedTiles)
        {
            if (playType.Equals(Play.Horizontal))
            {
                if (tile.pos_grid.x == 0) flag1 = true;
                else if (tile.pos_grid.x == Board.instance.RC_COUNT - 1) flag2 = true;
            }
            else if(playType.Equals(Play.Vertical))
            {
                if (tile.pos_grid.y == 0) flag1 = true;
                else if (tile.pos_grid.y == Board.instance.RC_COUNT - 1) flag2 = true;
            }
        }
        Board.instance.ResetVisited();

        return flag1 && flag2;
    }

    void CheckForConec(Tile current, Player owner, ref List<Tile> connectedTiles)
    {
        current.visited = true;
        connectedTiles.Add(current);

        foreach (Tile tile in Board.instance.GetAdjacentTiles(current.pos_grid))
        {
            if (!tile.visited)
            {
                if (tile.owner == null) continue;

                if (tile.owner == owner)
                {
                    CheckForConec(tile, owner, ref connectedTiles);
                }
            }
        }
    }
}

public enum Play
{
    Horizontal, Vertical
}
