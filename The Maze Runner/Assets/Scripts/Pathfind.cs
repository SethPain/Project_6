using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapGen;


public class Pathfind : MonoBehaviour {

    public GameObject map;
    public MapTile[,] tiles;
    public Node startNode;
    public Node goalNode;
    public Node current;
    private bool isSolved = false;
    private bool inDONE = false;
    private bool inTODO = false;
    private Node matched;
    List<Node> Solution = new List<Node>();
    List<Node> TODO;
    List<Node> DONE;



    private List<Node> WalkableAdj()
    {
        return new List<Node>();
    }

    // Use this for initialization
    void Start() {
        //A*
        tiles = map.GetComponent<Map>().getTiles();
        TODO = new List<Node>();
        DONE = new List<Node>();
        AStar();

        


        
	}

    
	// Update is called once per frame
	void Update () {
        //Traversal
        
	}

    void AStar()
    {
        for (int i = 0; i < 30; i++)
        {
            for (int y = 0; y < 30; y++)
            {
                if (tiles[i, y].IsStart)
                {
                    startNode = new Node(tiles[i, y], 0, 100, null);
                    TODO.Insert(0, startNode);
                }
                else if (tiles[i, y].IsGoal)
                {
                    goalNode = new Node(tiles[i, y]);
                }
            }
        }

        //int count = 0;
        //foreach (Node item in TODO)
        //{
        //    count++;
        //}
        //Debug.Log("The number of elements in TODO is: " + count);


        int count = 0;
        while (!isSolved)
        {
            count++;
            //Debug.Log("Every iteration: " + TODO[0]);
            current = TODO[0];
            Debug.Log("The first element is the start node: " + current.tile.IsStart);
            //Debug.Log("Is the first elment also equal to null? " + current == null);
            foreach (MapTile item in current.tile.Adjacents)
            {
                //Debug.Log(item);
                // $$ It appears that the list of adjacents don't include diagonals $$
                //if (item.X < current.tile.X || item.X > current.tile.X)
                //{
                //    if (item.Y < current.tile.Y || item.Y > current.tile.Y)
                //    {
                //        Debug.Log("Are there four of these?");
                //        continue;
                        
                //    }
                //}

                foreach (Node node in DONE)
                {
                    Debug.Log("This did a thing");
                    if (node.compareTile(item))
                        inDONE = true;
                }

                if (inDONE || !item.Walkable)
                {
                    Debug.Log("This did a thing");
                    inDONE = false;
                    continue;
                }

                foreach (Node node in TODO)
                {
                    Debug.Log("This did a thing");
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
        Debug.Log("The number of interations is: " + count);
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
}
