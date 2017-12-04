using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapGen;

public class Node : System.IEquatable<Node> {

    public MapTile tile;
    public float g;
    public float h;
    public float f;
    public Node parent;
    public Node child;


    public Node()
    {
        tile = new MapTile();
        g = 0;
        h = 0;
        f = g + h;
    }


    public Node(MapTile tile)
    {
        this.tile = tile;
        g = 0;
        h = 0;
        f = g + h;
        parent = new Node();
    }

    public Node(MapTile tile, float g, float h, Node parent)
    {
        this.tile = tile;
        this.g = g;
        this.h = h;
        f = g + h;
        this.parent = parent;
    }


    public bool Equals(Node node)
    {
        if (node.tile.X == tile.X && node.tile.Y == tile.Y)
            return true;
        else
            return false;
    }

    public string toString()
    {
        return "(" + tile.X + "," + tile.Y + ")";
    }

    public List<MapTile> adjacents(MapTile[,] map, int dimension)
    {
        List<MapTile> list = new List<MapTile>();
        if (tile.X + 1 < dimension)
            list.Add(map[tile.X + 1, tile.Y]);
        if (tile.X - 1 >= 0)
            list.Add(map[tile.X - 1, tile.Y]);
        if (tile.Y + 1 < dimension)
            list.Add(map[tile.X, tile.Y + 1]);
        if (tile.Y - 1 >= 0)
            list.Add(map[tile.X, tile.Y - 1]);

        return list;
    }
}
