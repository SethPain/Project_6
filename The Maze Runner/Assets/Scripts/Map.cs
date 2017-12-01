using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapGen;

public class Map : MonoBehaviour {

    
    public Transform wall;
    public Transform floor;
    public Transform start;
    public Transform goal;
    public Transform bound;
    public Transform player;

    // Use this for initialization
    void Start () {
        PrimGenerator prim = new PrimGenerator();

        // generate a map of size 30x30 with half of the walls removed after generation
        MapTile[,] tiles2 = prim.MapGen(30, 30, 0.15f);

        for (int i = 0; i < 30; i++)
        {
            Instantiate(bound, new Vector3(i, 2, -1), Quaternion.identity);
            Instantiate(bound, new Vector3(i, 2, 30), Quaternion.identity);
            for (int y = 0; y < 30; y++)
            {
                Instantiate(bound, new Vector3(-1, 2, y), Quaternion.identity);
                Instantiate(bound, new Vector3(30, 2, y), Quaternion.identity);
                if (tiles2[i, y].Walkable == true)
                {
                    if (tiles2[i, y].IsStart)
                    {
                        Instantiate(start, new Vector3(tiles2[i, y].X, .005f, tiles2[i, y].Y), Quaternion.identity);
                        Instantiate(player, new Vector3(tiles2[i, y].X, 1.5f, tiles2[i, y].Y), Quaternion.identity);
                    }
                    else if (tiles2[i, y].IsGoal)
                        Instantiate(goal, new Vector3(tiles2[i, y].X, .005f, tiles2[i, y].Y), Quaternion.identity);
                    else
                        Instantiate(floor, new Vector3(tiles2[i, y].X, .005f, tiles2[i, y].Y), Quaternion.identity);
                }
                else
                    Instantiate(wall, new Vector3(tiles2[i, y].X, 1, tiles2[i, y].Y), Quaternion.identity);
            }
        }
    }
	
	
}
