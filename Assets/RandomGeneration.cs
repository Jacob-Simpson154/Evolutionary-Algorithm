using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGeneration : MonoBehaviour
{
    public GameObject[] tile;
    TileController[,] tileNodes;

    //1 = dirt
    //2 = grass
    //3 = water

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

    private void Start()
    {
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                int i = spawnData[x, y];
                GameObject go = Instantiate(tile[i-1], transform);
                go.transform.position = new Vector3(x, 0, y);
            }
        }
    }

    //private void Start()
    //{
    //    tileNodes = new TileController[levelSize, levelSize];
    //    for (int y = 0; y < levelSize; y++)
    //    {
    //        for (int x = 0; x < levelSize; x++)
    //        {
    //            GameObject go = Instantiate(tile, transform);
    //            go.transform.position = new Vector3(x, 0, y);
    //            tileNodes[x, y] = go.GetComponent<TileController>();
    //        }
    //    }
    //}
}
