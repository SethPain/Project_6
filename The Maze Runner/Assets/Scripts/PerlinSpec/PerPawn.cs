using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapGen;

public class PerPawn : MonoBehaviour {

    public GameObject map;
    public MapTile[,] playerTiles;
    public Transform player;
    public MapTile[,] tiles;
    public Node startNode;
    public MapTile given;
    public Node goalNode;
    public Node current;
    private float timer;
    private int dimension;
    private float minF;
    private bool isSolved = false;
    private Node temp;
    List<Node> Solution = new List<Node>();
    List<Node> TODO;
    List<Node> DONE;
    bool isTiming = false;
    public int state = 0;
    bool pathing = true;

    void Awake()
    {
        map = GameObject.Find("Maze Generator");
        //dimension = map.GetComponent<Map>().dimension;
        player = GameObject.Find("Player").transform;



    }

    // Use this for initialization
    void Start()
    {
        playerTiles = map.GetComponent<PerMap>().getTiles();
        dimension = map.GetComponent<PerMap>().dimension;
        tiles = new MapTile[dimension, dimension];
        TODO = new List<Node>();
        DONE = new List<Node>();
        for (int i = 0; i < dimension; i++)
        {
            for (int y = 0; y < dimension; y++)
            {
                tiles[i, y] = new MapTile(playerTiles[i, y].X, playerTiles[i, y].Y);
                if (playerTiles[i, y].Walkable)
                {
                    tiles[i, y].Walkable = true;
                }
            }
        }
        tiles[(int)transform.position.x, (int)transform.position.y].IsStart = true;
        startNode = new Node(tiles[given.X, given.Y], 0, 0, null);
        TODO.Add(startNode);

        //StartCoroutine(AStar());







    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            //Sitting
            case 0:
                // Default state, doing nothing
                if ((player.transform.position - transform.position).magnitude <= 10)
                {
                    state = 1;
                }
                break;
            //Pathing
            case 1:
                goalNode = new Node(player.GetComponent<Perlin>().current.tile);
                //Debug.Log(goalNode.tile.X + " " + goalNode.tile.Y);
                StartCoroutine(AStar());
                state = 2;
                break;
            case 2:
                StartCoroutine(Go());
                if ((player.transform.position - transform.position).magnitude <= 3)
                {
                    state = 3;
                }
                break;
            case 3:
                StopAllCoroutines();
                break;

        }
        //Traversal
        if (isTiming)
        {
            timer += Time.deltaTime;
        }
    }

    //This will draw the optimal path in spheres
    public void OnDrawGizmos()
    {
        foreach (Node n in Solution)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(new Vector3(n.tile.X, 1, n.tile.Y), 0.25f);
        }
    }

    // This is the A* Algorithm, used to find an optimal path through the map
    IEnumerator AStar()
    {



        int count = 0;
        while (!isSolved)
        {
            count++;
            minF = 10000;
            foreach (Node node in TODO)
            {
                if (node.f < minF)
                {
                    minF = node.f;
                    current = node;
                }

            }


            foreach (MapTile item in current.adjacents(tiles, dimension))
            {

                //Debug.Log(current.tile.X + " , " + current.tile.Y);
                float g;
                if (current.Equals(startNode))
                    g = 10; // These are the adjacents of the start, so they have a g
                else
                {
                    g = current.parent.g + 10;
                }

                float h = (Mathf.Abs(goalNode.tile.X - current.tile.X) + Mathf.Abs(goalNode.tile.Y - current.tile.Y)) * 10;
                temp = new Node(item, g, h, current);



                if (DONE.Contains(temp) || !temp.tile.Walkable)// || TODO.Contains(temp)) //This for some reason breaks the code
                {
                    //Debug.Log("%%%%%%%%%%   TODO contains " + TODO.Count + " elements on count " + count + " on tile: " + temp.toString());
                    //Debug.Log("&&&&&&&&&&   DONE contains " + DONE.Count + " elements on count " + count + " on tile: " + temp.toString());
                    continue;
                }

                // No diagonal movement allowed, so not necessary
                //foreach (Node node in TODO)
                //{
                //    if (node.Equals(temp))
                //    {
                //        matched = node;
                //        if (!current.Equals(startNode) && g < matched.g)
                //        {
                //            matched.parent = current;
                //            matched.g = g;
                //            matched.f = matched.h + matched.g;
                //        }
                //    }
                //}

                TODO.Add(temp);
                if (current.Equals(goalNode))
                    isSolved = true;
            }
            //Debug.Log("Do we get here?");
            TODO.Remove(current);
            DONE.Add(current);


            yield return 0;
        }
        while (current.parent != null)
        {
            Solution.Add(current);
            current.parent.child = current;
            current = current.parent;
        }
        Solution.Reverse();
        //int counter = 0;
        //foreach (Node n in Solution)
        //{           
        //    Debug.Log("Step " + counter + ":" + n.toString());
        //    counter++;
        //}
    }

    // This is called once an optimal path is found to move the object along the path
    IEnumerator Go()
    {
        //if (index <= Path.Length)
        //    StartCoroutine(Travel(Path[index]));
        //yield return new WaitForEndOfFrame();



        try
        {
            foreach (Node node in Solution)
            {
                StartCoroutine(Travel(node));
                Solution.Remove(node);
            }
        }
        catch { }
        yield return new WaitForSeconds(10);

    }

    IEnumerator Travel(Node node)
    {
        beginTimer();
        while (transform.position != new Vector3(node.tile.X, transform.position.y, node.tile.Y))
        {
            transform.position += (new Vector3(node.tile.X, transform.position.y, node.tile.Y) - transform.position);
            yield return new WaitForSeconds(20);
            if (timer >= 20000)
            {
                endTimer();
            }
            yield return new WaitForSeconds(20);

        }
        //transform.position = new Vector3(node.tile.X, transform.position.y, node.tile.Y);
        //while (count < 200000)
        //{
        //    count++;
        //}


    }

    void beginTimer()
    {
        timer = 0;
        isTiming = true;
    }

    void endTimer()
    {
        isTiming = false;
    }
}
