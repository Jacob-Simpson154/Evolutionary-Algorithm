using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavigationController : MonoBehaviour
{
    [SerializeField] WorldGeneration worldGen;

    public Point Vector3ToPoint(Vector3 position)                           //Get point from world pos
    {
        float percentX = (position.x) / worldGen.worldPoints.GetLength(0);
        float percentY = (position.z) / worldGen.worldPoints.GetLength(1);
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((worldGen.worldPoints.GetLength(0)) * percentX);
        int y = Mathf.RoundToInt((worldGen.worldPoints.GetLength(1)) * percentY);
        return worldGen.worldPoints[x, y];
    }

    public List<Point> GetNeighboursWithDiagonal(Point point)                   //Return list of neighbours to point (with diaganol neighbours)
    {
        List<Point> neighbours = new List<Point>();
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if (x == 0 && y == 0)
                    continue;

                int checkX = (int)point.gridPos.x + x;
                int checkY = (int)point.gridPos.y + y;

                if (checkX >= 0 && checkX < worldGen.worldPoints.GetLength(0) && checkY >= 0 && checkY < worldGen.worldPoints.GetLength(1))
                {
                    neighbours.Add(worldGen.worldPoints[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }

    public int GetWorldPointLength()                                        //Return function for encapsulation of worldPoints
    {
        return worldGen.worldPoints.Length;
    }
}
