using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    public GameObject[] tile;

    public List<GameObject> generatedTiles;
    public Point[,] worldPoints;                                            //A 2D array of points the ai can walk


    int[,] spawnData = new int[10, 10] 
    {
        { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
        { 2, 2, 2, 2, 2, 2, 1, 2, 2, 2 },
        { 2, 2, 2, 2, 2, 1, 3, 1, 2, 2 },
        { 2, 2, 2, 2, 1, 3, 3, 1, 2, 2 },
        { 2, 2, 2, 2, 1, 3, 3, 3, 1, 2 },
        { 2, 2, 2, 2, 2, 1, 3, 3, 1, 2 },
        { 2, 2, 2, 2, 2, 2, 1, 1, 2, 2 },
        { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
        { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 },
        { 2, 2, 2, 2, 2, 2, 2, 2, 2, 2 }
    };


    public void GenerateWorld()
    {
        Clear();

        worldPoints = new Point[spawnData.GetLength(0), spawnData.GetLength(1)];


        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                int i = spawnData[x, y];

                GameObject go = Instantiate(tile[i - 1], transform);
                go.transform.position = new Vector3(x, 0, y);

                generatedTiles.Add(go);

                worldPoints[x, y] = go.GetComponent<Point>();
                worldPoints[x, y].SetPoint(go.GetComponent<TileController>().canTraverse, go.transform.position, x, y);
            }
        }
    }

    public void Clear()
    {
        if (generatedTiles.Count > 0)
        {
            foreach (GameObject item in generatedTiles)
            {
                DestroyImmediate(item);
            }
            generatedTiles.Clear();
        }
    }

    int gridSizeX, gridSizeY;


    private void Awake()
    {
        Setup();
    }

    void Setup()                                                            //Reset data lost from entering playmode
    {
        SetupSize();

        int count = 0;

        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                worldPoints[x, y] = generatedTiles[count].GetComponent<Point>();
                count++;
            }
        }
    }

    void SetupSize()                                                        //Calculate values
    {
        gridSizeX = spawnData.GetLength(0);          //How many pointsd can exist in the x component of worldPoints 2D array
        gridSizeY = spawnData.GetLength(1);          //How many pointsd can exist in the y component of worldPoints 2D array
        worldPoints = new Point[gridSizeX, gridSizeY];                      //Resize the worldPoints 2D array
    }
}
