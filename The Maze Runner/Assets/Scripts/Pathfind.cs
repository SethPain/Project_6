using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MapGen;


public class Pathfind : MonoBehaviour {

    public GameObject map;
    public MapTile[,] tiles;
    public Node startNode;
    public Node goalNode;
    public Node current;
    public float max_speed = 1.0f;
    public float slowing_distance = .5f;
    private float minF;
    private bool isSolved = false;
    private Node matched;
    private Node temp;
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
        for (int i = 0; i < 30; i++)
        {
            for (int y = 0; y < 30; y++)
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
        if (Input.GetKeyUp(KeyCode.Space))
        {
            SceneManager.LoadScene("Level_1");
        }
        //Traversal
        StartCoroutine(Travel());

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
            foreach (MapTile item in current.adjacents(tiles))
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

    IEnumerator Travel()
    {

        foreach(Node node in Solution)
        {
            //float startTime = Time.time;
            //float journeyLegth = Vector3.Distance(transform.position, new Vector3(node.tile.X, transform.position.y, node.tile.Y));
            //float distCovered = (Time.time - startTime) * speed;
            //float fracJourney = distCovered / journeyLegth;
            //transform.position = Vector3.Lerp(transform.position, new Vector3(node.tile.X, transform.position.y, node.tile.Y), fracJourney);
            transform.position += Arrive(node);
            yield return new WaitForSeconds(1);
        }
        //Vector3 goal = new Vector3(goalNode.tile.X, transform.position.y, goalNode.tile.Y);
        //while (transform.position != goal)
        //{
        //    Vector3 destination = new Vector3(Solution[Solution.Count - 1].tile.X, transform.position.y, Solution[Solution.Count - 1].tile.Y);
        //    transform.position += (destination - transform.position);
        //    if (transform.position == destination)
        //    {
        //        Solution.RemoveAt(Solution.Count - 1);
        //    }
        //    yield return new WaitForSeconds(1);
        //}
    }

    Vector3 Arrive(Node node)// , Vector3 velocity)
    {
        Vector3 target_offset = new Vector3(node.tile.X, transform.position.y, node.tile.Y) - transform.position;
        float distance = target_offset.magnitude;
        float ramped_speed = max_speed * (distance / slowing_distance);
        float clipped_speed = Mathf.Min(max_speed, ramped_speed);
        Vector3 desired_velocity = (clipped_speed / distance) * target_offset;
        // Vector3 steering = desired_velocity - velocity;
        return desired_velocity;
    }
}
