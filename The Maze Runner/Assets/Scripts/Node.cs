using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapGen;

public class Node : MonoBehaviour {

    public MapTile tile;
    public Vector3 location;
    public float g;
    public float h;
    public float f;
    public bool walkable;
    public Node parent;

    public Node()
    {
        tile = new MapTile();
        location = new Vector3();
        g = 0;
        h = 0;
        f = g + h;
        walkable = false;
    }
    

    public Node(MapTile tile)
    {
        this.tile = tile;
        this.location = new Vector3(tile.X, 0, tile.Y);
        g = 0;
        h = 0;
        f = g + h;
        parent = new Node();
        walkable = tile.Walkable;
    }

    public Node(MapTile tile, float g, float h, Node parent)
    {
        this.tile = tile;
        this.location = new Vector3(tile.X, 0, tile.Y);
        this.g = g;
        this.h = h;
        f = g + h;
        this.parent = parent;
        walkable = tile.Walkable;
    }


    public MapTile getTile()
    {
        return tile;
    }

    public bool compareTile(MapTile tile)
    {
        if (this.tile.CompareTo(tile) > 0)
            return true;
        else
            return false;
    } 
}
