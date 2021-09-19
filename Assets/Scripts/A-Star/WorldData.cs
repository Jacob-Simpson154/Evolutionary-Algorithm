using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class WorldData : MonoBehaviour
{
    public GameObject pointData;                                            //A gameobject containing the class Point(node)
    public Point[,] worldPoints;                                            //A 2D array of points the ai can walk
    public LayerMask walkable;                                              //What layer account for walkable terrain (layermask "Terrain")
    public Vector2 worldSize;                                               //The world size (needs to cover the entire world - see WireFrame gizmo)
    const float pointRadius = 2;                                            //How big is the walkable point (greater this value the less overall points)

    float pointDiameter;
    int gridSizeX, gridSizeY;

    [SerializeField]
    List<GameObject> points = new List<GameObject>();

    private void Start()
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
                worldPoints[x, y] = points[count].GetComponent<Point>();
                count++;
            }
        }
    }

    void SetupSize()                                                        //Calculate values
    {
        pointDiameter = pointRadius * 2;                                    //The actual size of each point
        gridSizeX = Mathf.RoundToInt(worldSize.x / pointDiameter);          //How many pointsd can exist in the x component of worldPoints 2D array
        gridSizeY = Mathf.RoundToInt(worldSize.y / pointDiameter);          //How many pointsd can exist in the y component of worldPoints 2D array
        worldPoints = new Point[gridSizeX, gridSizeY];                      //Resize the worldPoints 2D array
    }

    public Point Vector3ToPoint(Vector3 position)                           //Get point from world pos
    {
        float percentX = (position.x + worldSize.x / 2) / worldSize.x;
        float percentY = (position.z + worldSize.y / 2) / worldSize.y;
        percentX = Mathf.Clamp01(percentX);
        percentY = Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);
        return worldPoints[x, y];
    }

    public List<Point> GetNeighboursNonDiagonal(Point point)                    //Return list of neighbours to point (no diaganol neighbours)
    {
        List<Point> neighbours = new List<Point>();

        int x = (int)point.gridPos.x;
        int y = (int)point.gridPos.y;

        //Up
        if (worldPoints[x, y + 1])
            neighbours.Add(worldPoints[x, y + 1]);

        //Down
        if (worldPoints[x, y - 1])
            neighbours.Add(worldPoints[x, y - 1]);

        //Left
        if (worldPoints[x - 1, y])
            neighbours.Add(worldPoints[x - 1, y]);

        //Right
        if (worldPoints[x + 1, y])
            neighbours.Add(worldPoints[x + 1, y]);

        return neighbours;
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

                if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                {
                    neighbours.Add(worldPoints[checkX, checkY]);
                }
            }
        }
        return neighbours;
    }

    public void Generate()                                                      //Editor script for generation of the WorldPoints array
    {
        Clear();
        SetupSize();

        Stopwatch timer = new Stopwatch();
        timer.Start();

        Vector3 start = transform.position - Vector3.right * worldSize.x / 2 - Vector3.forward * worldSize.y / 2;
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                Vector3 worldPoint = start + Vector3.right * (x * pointDiameter + pointRadius) + Vector3.forward * (y * pointDiameter + pointRadius) + new Vector3(0, 100, 0);
                RaycastHit hit;
                if (Physics.Raycast(worldPoint, Vector3.down, out hit, Mathf.Infinity))
                {
                    //Prerequisites
                    bool checkWalkable = (Physics.CheckSphere(hit.point, pointRadius, walkable));

                    GameObject go = Instantiate(pointData, transform);

                    go.transform.position = hit.point + new Vector3(0, 0.1f, 0);
                    Point thisPoint = go.GetComponent<Point>();
                    thisPoint.SetPoint(checkWalkable, hit.point, x, y);

                    points.Add(go);
                    worldPoints[x, y] = thisPoint;
                }

                else
                {
                    GameObject go = Instantiate(pointData, transform);

                    go.transform.position = new Vector3(worldPoint.x, 0, worldPoint.z);
                    Point thisPoint = go.GetComponent<Point>();
                    thisPoint.SetPoint(false, go.transform.position, x, y);

                    points.Add(go);
                    worldPoints[x, y] = thisPoint;
                }
            }
        }

        timer.Stop();
        print("Generation of " + points.Count + " traversible points took: " + timer.ElapsedMilliseconds + " ms");
    }

    public void Clear()                                                     //Editor script for deleting all points in WorldPoints array
    {
        int count = 0;
        foreach (GameObject item in points)
        {
            DestroyImmediate(item);
            count++;
        }
        points.Clear();
        worldPoints = null;
        print("Deleted " + count + " points");
    }

    private void OnDrawGizmos()                                             //Visualises the area in which points will be generated
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(worldSize.x, 1, worldSize.y));
        if (worldPoints == null && points.Count > 0)
        {
            Setup();
        }
    }

    public int GetWorldPointLength()                                        //Return function for encapsulation of worldPoints
    {
        return worldPoints.Length;
    }
}
