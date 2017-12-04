using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MapGen;


public class Pathfind : MonoBehaviour {

    public GameObject map;
    public GameObject projectile;
    public MapTile[,] tiles;
    public Node startNode;
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



    // Use this for initialization
    void Start() {
        tiles = map.GetComponent<Map>().getTiles();
        dimension = map.GetComponent<Map>().dimension;
        TODO = new List<Node>();
        DONE = new List<Node>();
        for (int i = 0; i < dimension; i++)
        {
            for (int y = 0; y < dimension; y++)
            {
                if (tiles[i, y].IsStart)
                {
                    startNode = new Node(tiles[i, y], 0, 0, null);
                    TODO.Add(startNode);
                }
                else if (tiles[i, y].IsGoal)
                {
                    goalNode = new Node(tiles[i, y]);
                }
            }
        }
        StartCoroutine(AStar());

        





    }
    
	// Update is called once per frame
	void Update () {
        switch (state)
        {
            //Pathfinding
            case 0:
                // Default state, using A* to find goal
                if (!pathing)
                    StartCoroutine(AStar());
                // if (enemy detected)
                break;
            //Fighting
            case 1:
                Instantiate(projectile, transform.position, Quaternion.identity);
                // if (enemy dead)
                break;

        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SceneManager.LoadScene("Level_1");
        }
        //Traversal
        StartCoroutine(Go());
        if (isTiming)
        {
            timer += Time.deltaTime;
        }
        if (transform.position == new Vector3(goalNode.tile.X, transform.position.y, goalNode.tile.Y))
            SceneManager.LoadScene("Level_1");
    }

    //This will draw the optimal path in spheres
    public void OnDrawGizmos()
    {
        foreach (Node n in Solution)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(new Vector3(n.tile.X, 1, n.tile.Y), 0.25f);
        }
    }

    // This is the A* Algorithm, used to find an optimal path through the map
    IEnumerator AStar()
    {
        

        //int count = 0;
        //foreach (Node item in TODO)
        //{
        //    count++;
        //}
        //Debug.Log("The number of elements in TODO is: " + count);


        int count = 0;
        while (!isSolved)
        {
            if (count > Mathf.Pow(dimension, 2))
                SceneManager.LoadScene("Level_1");
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


            //Debug.Log("The first element is the start node: " + current.tile.IsStart);
            //Debug.Log("Is the first elment also equal to null? " + current == null);
            foreach (MapTile item in current.adjacents(tiles, dimension))
            {

                //Debug.Log(current.tile.X + " , " + current.tile.Y);
                //Debug.Log("The adjacents are: " + item.IsStart);
                float g;
                if (current.Equals(startNode))
                    g = 10; // These are the adjacents of the start, so they have a g
                else
                    g = current.parent.g + 10;
                float h = (Mathf.Abs(goalNode.tile.X - current.tile.X) + Mathf.Abs(goalNode.tile.Y - current.tile.Y))*10;
                temp = new Node(item, g, h, current);
                


                if (DONE.Contains(temp) || !temp.tile.Walkable )//|| TODO.Contains(temp)) //This for some reason breaks the code
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
            TODO.Remove(current);
            DONE.Add(current);


            yield return 0;
        }
        Debug.Log(goalNode.tile.X);
        Debug.Log(goalNode.tile.Y);
        Debug.Log(current.tile.X);
        Debug.Log(current.tile.Y);
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
                current = node;
                StartCoroutine(Travel(node));
                Solution.Remove(node);
            }
        } catch { }
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
