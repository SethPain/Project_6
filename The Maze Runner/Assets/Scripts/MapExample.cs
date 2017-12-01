using UnityEngine;
using System.Collections;
// includes the MapGen namespace methods
using MapGen;

public class MapExample : MonoBehaviour
{
    public int choice;
    public Transform wall;
    public Transform floor;
    public Transform start;
    public Transform goal;

	void Start ()
    {
        PrimGenerator primGen = new PrimGenerator();
        PerlinGenerator perlinGen = new PerlinGenerator();

        switch (choice)
        {

            case 1:
                // generate a map of size 20x20 with no extra walls removed
                MapTile[,] tiles1 = primGen.MapGen(20, 20, 0.25f);
                for (int i = 0; i < 20; i++)
                {
                    for (int y = 0; y < 20; y++)
                    {
                        if (tiles1[i, y].Walkable == true)
                        {
                            if (tiles1[i, y].IsStart)
                                Instantiate(start, new Vector3(tiles1[i, y].X, .005f, tiles1[i, y].Y), Quaternion.identity);
                            else if (tiles1[i, y].IsGoal)
                                Instantiate(goal, new Vector3(tiles1[i, y].X, .005f, tiles1[i, y].Y), Quaternion.identity);
                            else
                                Instantiate(floor, new Vector3(tiles1[i, y].X, .005f, tiles1[i, y].Y), Quaternion.identity);
                        }
                        else
                            Instantiate(wall, new Vector3(tiles1[i, y].X, 1, tiles1[i, y].Y), Quaternion.identity);
                    }
                }
                break;

            case 2:
                // generate a map of size 30x30 with half of the walls removed after generation
                MapTile[,] tiles2 = primGen.MapGen(30, 30, 0.15f);
                for (int i = 0; i < 30; i++)
                {
                    for (int y = 0; y < 30; y++)
                    {
                        if (tiles2[i, y].Walkable == true)
                        {
                            if (tiles2[i, y].IsStart)
                                Instantiate(start, new Vector3(tiles2[i, y].X, .005f, tiles2[i, y].Y), Quaternion.identity);
                            else if (tiles2[i, y].IsGoal)
                                Instantiate(goal, new Vector3(tiles2[i, y].X, .005f, tiles2[i, y].Y), Quaternion.identity);
                            else
                                Instantiate(floor, new Vector3(tiles2[i, y].X, .005f, tiles2[i, y].Y), Quaternion.identity);
                        }
                        else
                            Instantiate(wall, new Vector3(tiles2[i, y].X, 1, tiles2[i, y].Y), Quaternion.identity);
                    }
                }
                break;
                
            case 3:
                // generates a map of size 20x20 with a large constraint (generates a tightly-packed map)
                MapTile[,] tiles3 = perlinGen.MapGen(20, 20, 5.0f);
                for (int i = 0; i < 20; i++)
                {
                    for (int y = 0; y < 20; y++)
                    {
                        if (tiles3[i, y].Walkable == true)
                        {
                            if (tiles3[i, y].IsStart)
                                Instantiate(start, new Vector3(tiles3[i, y].X, .005f, tiles3[i, y].Y), Quaternion.identity);
                            else if (tiles3[i, y].IsGoal)
                                Instantiate(goal, new Vector3(tiles3[i, y].X, .005f, tiles3[i, y].Y), Quaternion.identity);
                            else
                                Instantiate(floor, new Vector3(tiles3[i, y].X, .005f, tiles3[i, y].Y), Quaternion.identity);
                        }
                        else
                            Instantiate(wall, new Vector3(tiles3[i, y].X, 1, tiles3[i, y].Y), Quaternion.identity);
                    }
                }
                break;

            case 4:
                // generates a map of size 20x20 with a medium constraint (generates a more open map)
                MapTile[,] tiles4 = perlinGen.MapGen(20, 20, 10.0f);
                for (int i = 0; i < 20; i++)
                {
                    for (int y = 0; y < 20; y++)
                    {
                        if (tiles4[i, y].Walkable == true)
                        {
                            if (tiles4[i, y].IsStart)
                                Instantiate(start, new Vector3(tiles4[i, y].X, .005f, tiles4[i, y].Y), Quaternion.identity);
                            else if (tiles4[i, y].IsGoal)
                                Instantiate(goal, new Vector3(tiles4[i, y].X, .005f, tiles4[i, y].Y), Quaternion.identity);
                            else
                                Instantiate(floor, new Vector3(tiles4[i, y].X, .005f, tiles4[i, y].Y), Quaternion.identity);
                        }
                        else
                            Instantiate(wall, new Vector3(tiles4[i, y].X, 1, tiles4[i, y].Y), Quaternion.identity);
                    }
                }
                break;

            case 5:
                // generates a map of size 20x20 with a small constraint (generates a very open map)
                MapTile[,] tiles5 = perlinGen.MapGen(20, 20, 20.0f);
                for (int i = 0; i < 20; i++)
                {
                    for (int y = 0; y < 20; y++)
                    {
                        if (tiles5[i, y].Walkable == true)
                        {
                            if (tiles5[i, y].IsStart)
                                Instantiate(start, new Vector3(tiles5[i, y].X, .005f, tiles5[i, y].Y), Quaternion.identity);
                            else if (tiles5[i, y].IsGoal)
                                Instantiate(goal, new Vector3(tiles5[i, y].X, .005f, tiles5[i, y].Y), Quaternion.identity);
                            else
                                Instantiate(floor, new Vector3(tiles5[i, y].X, .005f, tiles5[i, y].Y), Quaternion.identity);
                        }
                        else
                            Instantiate(wall, new Vector3(tiles5[i, y].X, 1, tiles5[i, y].Y), Quaternion.identity);
                    }
                }
                break;
        }
        
        
    }
}
