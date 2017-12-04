using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapGen;

public class Map : MonoBehaviour {

    public string primOrPerlin = "Prim";
    public int dimension = 30;
    public float removOrConstrain = 0.15f;
    public GameObject wall;
    public GameObject floor;
    public GameObject start;
    public GameObject goal;
    public GameObject bound;
    public GameObject player;
    PrimGenerator prim;
    PerlinGenerator perlin;
    MapTile[,] tiles;

    void Awake()
    {
        
        prim = new PrimGenerator();
        perlin = new PerlinGenerator();

        // generate a map of size 30x30 with half of the walls removed after generation
        if (primOrPerlin == "Prim" || primOrPerlin == "prim")
            tiles = prim.MapGen(dimension, dimension, removOrConstrain);
        else
            tiles = perlin.MapGen(dimension, dimension, removOrConstrain);
    }

    // Use this for initialization
    void Start() {
        

        

        for (int i = 0; i < dimension; i++)
        {
            Instantiate(bound, new Vector3(i, 2, -1), Quaternion.identity);
            Instantiate(bound, new Vector3(i, 2, dimension), Quaternion.identity);
            for (int y = 0; y < dimension; y++)
            {
                Instantiate(bound, new Vector3(-1, 2, y), Quaternion.identity);
                Instantiate(bound, new Vector3(dimension, 2, y), Quaternion.identity);
                if (tiles[i, y].Walkable == true)
                {
                    if (tiles[i, y].IsStart)
                    {
                        Instantiate(start, new Vector3(tiles[i, y].X, .005f, tiles[i, y].Y), Quaternion.identity);
                        player.transform.position = new Vector3(tiles[i, y].X, 1, tiles[i, y].Y);
                    }
                    else if (tiles[i, y].IsGoal)
                        Instantiate(goal, new Vector3(tiles[i, y].X, .005f, tiles[i, y].Y), Quaternion.identity);
                    else
                        Instantiate(floor, new Vector3(tiles[i, y].X, .005f, tiles[i, y].Y), Quaternion.identity);
                }
                else
                    Instantiate(wall, new Vector3(tiles[i, y].X, 1, tiles[i, y].Y), Quaternion.identity);
            }
        }
    }

    public MapTile[,] getTiles()
    {
        return tiles;
    }
	
	
}
