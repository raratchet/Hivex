using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    public GameObject tile_prefab;
    public Vector2 d_offset;
    public Vector2 r_offset;
    public List<Tile> tiles;

    public int RC_COUNT = 7;

    #region Singleton

    public static Board instance;
    public void Awake()
    {
        instance = this;
        GenerateTiles();
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SelectTile(Tile tile)
    {
        tile.GetSelected();    
    }

    //Reinicia el valor de visited de todas las tiles
    public void ResetVisited()
    {
        foreach(var tile in tiles)
        {
            tile.visited = false;
        }
    }

    public List<Tile> GetAdjacentTiles(Vector2 pos)
    {
        Tile[] a_tiles = new Tile[6];

        Vector2 temp = Vector2.zero;
        temp.Set(pos.x, pos.y - 1);
        a_tiles[0] = GetTileAt(temp);
        temp.Set(pos.x + 1, pos.y - 1);
        a_tiles[1] = GetTileAt(temp);
        temp.Set(pos.x - 1, pos.y);
        a_tiles[2] = GetTileAt(temp);
        temp.Set(pos.x + 1, pos.y);
        a_tiles[3] = GetTileAt(temp);
        temp.Set(pos.x - 1, pos.y + 1);
        a_tiles[4] = GetTileAt(temp);
        temp.Set(pos.x, pos.y + 1);
        a_tiles[5] = GetTileAt(temp);

        List<Tile> adj = new List<Tile>();

        foreach(var tile in a_tiles)
        {
            if (tile != null)
            {
                adj.Add(tile);
            }

        }

        return adj;
    }

    public Tile GetTileAt(Vector2 pos)
    {
        if(pos.x >= 0 && pos.x < RC_COUNT)
            if (pos.y >= 0 && pos.y < RC_COUNT)
            {
                int t_pos = (int)((pos.x * RC_COUNT) + pos.y);

                return tiles.ElementAt(t_pos);
            }
         return null;
    }

    void GenerateTiles()
    {
        Vector2 rightoffset = Vector2.zero;
        Vector2 downoffset = Vector2.zero;
        for(int j = 0; j < RC_COUNT; j++)
        {
            for (int i = 0; i < RC_COUNT; i++)
            {
                Tile generated = Instantiate(tile_prefab).GetComponent<Tile>();
                tiles.Add(generated);
                generated.pos = (RC_COUNT * j) + i;
                generated.pos_grid = new Vector2(j, i);
                generated.transform.position = downoffset + rightoffset;
                generated.transform.parent = transform;
                downoffset += d_offset;
                if(j == 0)
                {
                    generated.type = TileType.LEFT_EDGE;
                }else if( j == RC_COUNT - 1)
                {
                    generated.type = TileType.RIGHT_EDGE;
                }
                else if (i == 0)
                {
                    generated.type = TileType.TOP_EDGE;
                }
                else if (i == RC_COUNT - 1)
                {
                    generated.type = TileType.DOWN_EDGE;
                }
                else
                {
                    generated.type = TileType.MIDDLE;
                }
            }
            downoffset = Vector2.zero;
            rightoffset += r_offset;
        }
    }
}
