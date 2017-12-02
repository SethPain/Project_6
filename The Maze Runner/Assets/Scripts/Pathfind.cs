using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapGen;


public class Pathfind : MonoBehaviour {

    public GameObject map;
    public MapTile[,] tiles;
    private List<MapTile> adjacents;
    public Node startNode;
    public Node goalNode;
    public Node current;
    private bool isSolved = false;
    private bool inDONE = false;
    private bool inTODO = false;
    private Node matched;
    List<Node> Solution = new List<Node>();



    private List<Node> WalkableAdj()
    {
        return new List<Node>();
    }

    // Use this for initialization
    void Start() {
        //A*
        tiles = map.GetComponent<Map>().getTiles();
        List<Node> TODO = new List<Node>();
        List<Node> DONE = new List<Node>();

        for (int i = 0; i < 30; i++)
        {
            for (int y = 0; y < 30; y++)
            {
                if (tiles[i, y].IsStart)
                {
                    startNode = new Node(tiles[i, y], 0, 100, null);
                    TODO.Add(startNode);
                }
                else if (tiles[i, y].IsGoal)
                {
                    goalNode = new Node(tiles[i, y]);
                }
            }
        }

        int count = 0;
        foreach (Node item in TODO)
        {
            count++;
        }
        Debug.Log("The number of elements in TODO is: " + count);

        

        while (!isSolved)
        {
            Debug.Log("Every iteration: " + TODO[0]);
            Debug.Log("Every iteration: " + TODO.IndexOf(new Node()));
            current = TODO[0];
            foreach (MapTile item in current.tile.Adjacents)
            {
                if (item.X < current.tile.X || item.X > current.tile.X)
                {
                    if (item.Y < current.tile.Y || item.Y > current.tile.Y)
                    {
                        continue;
                    }
                }

                foreach (Node node in DONE)
                {
                    if (node.compareTile(item))
                        inDONE = true;
                }

                if(inDONE || !item.Walkable)
                {
                    inDONE = false;
                    continue;
                }

                foreach (Node node in TODO)
                {
                    if (node.compareTile(item))
                    {
                        inTODO = true;
                        matched = node;
                    }
                }
                float g;
                if (current.Equals(startNode))
                    g = 1;
                else
                    g = current.parent.g + 1;
                float h = (new Vector3(item.X, 0, item.Y) - new Vector3(goalNode.tile.X, 0, goalNode.tile.Y)).magnitude;
                if (!inTODO)
                {
                    
                    TODO.Add(new Node(item, g, h, current));
                    TODO.RemoveAt(0);
                    DONE.Add(current);
                }
                if (!current.Equals(startNode) && g < matched.g)
                {
                    matched.parent = current;
                    matched.g = g;
                    matched.f = matched.h + matched.g;
                }
                inTODO = false;
                if (TODO.IndexOf(goalNode) >= 0)
                    Debug.Log("This is the index of the goal node: " + TODO.IndexOf(goalNode));
                    isSolved = true;
            }

        
        }
        Debug.Log(goalNode.tile.X);
        Debug.Log(goalNode.tile.Y);
        Debug.Log(current.tile.X);
        Debug.Log(current.tile.Y);
        while (current.parent != null)
        {
            Solution.Add(current);
            current = current.parent;
        }
        // Solution.Reverse();


        
	}

    
	// Update is called once per frame
	void Update () {
        //Traversal
        
	}
}
