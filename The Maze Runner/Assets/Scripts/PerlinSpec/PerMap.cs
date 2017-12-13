using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MapGen;

public class PerMap : MonoBehaviour {

    public string primOrPerlin = "Perlin";
    public int dimension = 30;
    public float removOrConstrain = 20f;
    public GameObject wall;
    public GameObject floor;
    public GameObject start;
    public GameObject goal;
    public GameObject bound;
    public GameObject player;
    public GameObject pawn;
    public GameObject boss;
    int pawn_count = 2;
    PrimGenerator prim;
    PerlinGenerator perlin;
    MapTile[,] tiles;
    MapTile starttile;
    MapTile goaltile;

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
    void Start()
    {




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
                        starttile = tiles[i, y];
                        Instantiate(start, new Vector3(tiles[i, y].X, .005f, tiles[i, y].Y), Quaternion.identity);
                        player.transform.position = new Vector3(tiles[i, y].X, 1, tiles[i, y].Y);
                    }
                    else if (tiles[i, y].IsGoal)
                    {
                        goaltile = tiles[i, y];
                        Instantiate(goal, new Vector3(tiles[i, y].X, .005f, tiles[i, y].Y), Quaternion.identity);
                        Instantiate(boss, new Vector3(tiles[i, y].X, 1, tiles[i, y].Y), Quaternion.identity);
                    }
                    else
                    {
                        Instantiate(floor, new Vector3(tiles[i, y].X, .005f, tiles[i, y].Y), Quaternion.identity);
                    }

                }
                else
                    Instantiate(wall, new Vector3(tiles[i, y].X, 1, tiles[i, y].Y), Quaternion.identity);
            }
        }

        while (pawn_count > 0)
        {
            int Hor = Random.Range(0, dimension);
            int Ver = Random.Range(0, dimension);
            if (tiles[Hor, Ver].Walkable && !tiles[Hor, Ver].IsStart && !tiles[Hor, Ver].IsGoal)
            {
                GameObject temp = Instantiate(pawn, new Vector3(tiles[Hor, Ver].X, 1, tiles[Hor, Ver].Y), Quaternion.identity);
                temp.GetComponent<PerPawn>().given = tiles[Hor, Ver];
                pawn_count--;
            }
        }

    }

    public MapTile[,] getTiles()
    {
        return tiles;
    }

}
