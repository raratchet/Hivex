using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA_Player : Player
{
    Tile startPoint;
    Tile endPoint;

    public List<Tile> myPath;

    //La IA por ahora siempre tira en vertical
    void FirstTurn()
    {
        //Probar si tiro en vertical u horizontal
        int randomPoint = Random.Range(0, Board.instance.RC_COUNT);
        Vector2 pos = new Vector2(randomPoint, 0);
        startPoint = Board.instance.GetTileAt(pos);
        pos.y = Board.instance.RC_COUNT - 1;
        endPoint = Board.instance.GetTileAt(pos);
        myPath = A_star(startPoint, endPoint);
    }

    // Start is called before the first frame update
    void Start()
    {
        if(Board.instance != null)
        {
            Load();
        }
    }

    public override void Load()
    {
        FirstTurn();
    }

    // Update is called once per frame
    void Update()
    {
        if(TurnManager.instance.CurrentPlayer == this)
        {
            if(endPoint.owner != null)
            {
                BuildNewPath();
            }

            if(myPath.Count > 0)
            {
                Tile nextTile = myPath[0];
                if(nextTile.owner != null)
                {
                    if (nextTile == startPoint)
                        FirstTurn();
                    else
                        BuildNewPath();
                }
                Board.instance.SelectTile(nextTile);
                myPath.Remove(nextTile);
            }
        }
    }

    void BuildNewPath()
    {
        List<Tile> bottomRow = new List<Tile>();
        Vector2 pos = new Vector2(0, Board.instance.RC_COUNT - 1);
        for(int i = 0; i < Board.instance.RC_COUNT; i++)
        {
            pos.x = i;
            Tile aTile = Board.instance.GetTileAt(pos);
            if(aTile.owner == null)
            {
                bottomRow.Add(aTile);
            }
        }

        int randomBottom = Random.Range(0, bottomRow.Count);

        endPoint = bottomRow[randomBottom];

        List<Tile> newPath = A_star(startPoint, endPoint);
        myPath.Clear();

        foreach(Tile tile in newPath)
        {
            if(tile.owner == null)
            {
                myPath.Add(tile);
            }
        }

        Debug.Log("Cambie mi punto final " + endPoint);
    }

    public List<Tile> A_star(Tile start, Tile end)
    {
        List<Tile> nodesToTest = new List<Tile>();
        List<Tile> testedNodes = new List<Tile>();
        Dictionary<Tile, Tile> parentFor = new Dictionary<Tile, Tile>(); //Guarda el padre para nodo a que es nodo b

        List<Tile> path = new List<Tile>();

        nodesToTest.Add(start);

        Dictionary<Tile, float> globalScore = new Dictionary<Tile, float>();
        Dictionary<Tile, float> localScore = new Dictionary<Tile, float>();

        globalScore[start] = tempheus(start, end);
        localScore[start] = 0;


        while (nodesToTest.Count > 0)
        {
            Tile current = null;
            //Encontrar el globalScore mas pequeño en nodesToTest
            for (int i = 0; i < nodesToTest.Count; i++)
            {
                Tile node = nodesToTest[i];

                if (!globalScore.ContainsKey(node))
                {
                    globalScore[node] = 999999;
                }
                if (!localScore.ContainsKey(node))
                {
                    localScore[node] = 999999;
                }

                if (!current)
                    current = node;

                if (localScore[node] < localScore[current])
                    current = node;

            }

            if (current == end)
                return reconstruct_path(ref parentFor, start, current); //Devolver la ruta;

            nodesToTest.Remove(current);
            testedNodes.Add(current);

            List<Tile> neighbors = Board.instance.GetAdjacentTiles(current.pos_grid);

            for (int i = 0; i < neighbors.Count; i++)
            {

                Tile neighbor = neighbors[i];

                if (neighbor.owner != this && neighbor.owner != null) testedNodes.Add(neighbor);

                if (testedNodes.Contains(neighbor)) continue;

                if (!globalScore.ContainsKey(neighbor))
                {
                    globalScore[neighbor] = 999999;
                }
                if (!localScore.ContainsKey(neighbor))
                {
                    localScore[neighbor] = 999999;
                }

                int tempG = (int)(localScore[current] + 1);

                if (tempG < localScore[neighbor])
                {
                    localScore[neighbor] = tempG;
                    globalScore[neighbor] = localScore[neighbor] + tempheus(neighbor, end);
                    parentFor[neighbor] = current;
                    nodesToTest.Add(neighbor);
                }
            }
        }

        return path;
    }

    float tempheus(Tile node, Tile end)
    {
        float a = Vector2.Distance(node.pos_grid,end.pos_grid);
        return a;
    }

    List<Tile> reconstruct_path(ref Dictionary<Tile, Tile> parentOf, Tile start, Tile end)
    {
        List<Tile> path = new List<Tile>();
        path.Add(end);

        Tile tmp = parentOf[end];

        while (tmp != start && tmp != null)
        {
            path.Add(tmp);
            tmp = parentOf[tmp];
        }
        path.Add(start);

        path.Reverse();

        return path;
    }
}

