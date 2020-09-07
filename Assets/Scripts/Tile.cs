using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    public SpriteRenderer _renderer;
    public Board _board;
    public Vector2 pos_grid;
    public int pos;
    public Player owner = null;
    public SpriteRenderer token;
    public Sprite sprite;
    public TileType type;
    public bool visited = false;


    private void Awake()
    {
        _board = Board.instance;
    }

    private void OnMouseDown()
    {
        GetSelected();
    }

    public void GetSelected()
    {
        // CASILLA TIENE DUEÑO?
        // PREGUNTAR AL TURNMANAGER EL JUGADOR ACUTAL
        // ASIGNAR EL JUAGDOR AL DUEÑO DE ESTA CASILLA
        // INDICAR AL MANAGER QUE SE HIZO UNA SELECCION
        if (owner == null)
        {
            owner = TurnManager.instance.CurrentPlayer;
            token.sprite = TurnManager.instance.CurrentPlayer.icon;
            SendMessageUpwards("TileSelection", this);
        }
    }

}

public enum TileType
{
    MIDDLE , RIGHT_EDGE, LEFT_EDGE, TOP_EDGE, DOWN_EDGE
}
