using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapGen;

public class Pathfind : MonoBehaviour {
    public GameObject map;
    public MapTile[,] tiles;
    private List<MapTile> adjacents;
    ArrayList list = new ArrayList();

    class Node
    {
        public Vector3 location;
        public float g;
        public float h;
        public float f;
        public Node parent;

        public Node() { }

        public Node(Vector3 location, float g, float h, Node parent)
        {
            this.location = location;
            this.g = g;
            this.h = h;
            f = g + h;
            this.parent = parent;
        }

        public Node(MapTile tile)
        {
            this.location = new Vector3(tile.X, 0, tile.Y);
        }
    }
    private List<Node> WalkableAdj()
    {
        return new List<Node>();
    }

	// Use this for initialization
	void Start () {
        //A*

        tiles = map.GetComponent<Map>().tiles;
	}
	
	// Update is called once per frame
	void Update () {
		//Traversal 
	}
}
