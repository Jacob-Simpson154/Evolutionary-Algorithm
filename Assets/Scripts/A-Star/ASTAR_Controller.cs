using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System;

public class ASTAR_Controller : MonoBehaviour
{
    NavigationController world;
    Heuristics heuristics;
    private void Awake()
    {
        world = GetComponent<NavigationController>();
        heuristics = new Heuristics();
    }

    public void FindPath(PathRequest request, Action<PathResult> callback)              //Find path is called by an AI agent
    {
        Stopwatch timer = new Stopwatch();                                              //Record the time taken to find path
        timer.Start();

        Vector3[] waypoints = new Vector3[0];
        bool pathSuccess = false;

        Point startPoint = world.Vector3ToPoint(request.pathStart);                     //Get the point from the agent calling function
        Point endPoint = world.Vector3ToPoint(request.pathEnd);                         //Get the point from the target destination
        startPoint.parent = startPoint;

        if(endPoint.walkable == false)
        {
            Point closestNeighbour = null;
            foreach (Point neighbour in world.GetNeighboursWithDiagonal(endPoint))
            {
                if (neighbour.walkable && closestNeighbour == null)
                    closestNeighbour = neighbour;

                if(neighbour.walkable && closestNeighbour != null)
                {
                    if(Vector3.Distance(startPoint.transform.position, neighbour.transform.position) < Vector3.Distance(startPoint.transform.position, closestNeighbour.transform.position))
                        closestNeighbour = neighbour;
                }
            }

            if (closestNeighbour != null)
                endPoint = closestNeighbour;
        }

        if (startPoint.walkable && endPoint.walkable)
        {
                Heap<Point> openSet = new Heap<Point>(world.GetWorldPointLength());     //Points that are yet to be checked
                List<Point> closedSet = new List<Point>();                              //Points that have been checked

                openSet.Add(startPoint);                                                //Start by examining the first point

                while (openSet.Count > 0)                                               //If openset is greater than 0 path there are still points to explore
                {
                    Point current = openSet.RemoveFirst();
                    closedSet.Add(current);

                    if (current == endPoint)                                            //If the point being examined is end point then there is at least one path to the goal
                    {
                        timer.Stop();

                        print("Path generation took: " + timer.ElapsedMilliseconds + " ms");
                        pathSuccess = true;

                        break;
                    }

                foreach (Point neighbour in world.GetNeighboursWithDiagonal(current))   //Explore all neighbours of the current point
                {
                    if (!neighbour.walkable || closedSet.Contains(neighbour))           //If the neighbour isn't walkable or if we have already examined it - skip
                    {
                        continue;
                    }


                    int newMovementCostToNeightbour = current.gCost + heuristics.ManhattanDistance(current, neighbour);
                    if (newMovementCostToNeightbour < neighbour.gCost || !openSet.Contains(neighbour))
                    {
                        neighbour.gCost = newMovementCostToNeightbour;
                        neighbour.hCost = heuristics.ManhattanDistance(neighbour, endPoint);
                        neighbour.parent = current;

                        if (!openSet.Contains(neighbour))
                        {
                            openSet.Add(neighbour);
                        }
                        else openSet.UpdateItem(neighbour);
                    }
                }
            }
            
            if (pathSuccess)
            {
                waypoints = ConvertToWaypoints(startPoint, endPoint);
                pathSuccess = waypoints.Length > 0;
            } else
            {
                print("Path not found");
            }
            callback(new PathResult(waypoints, pathSuccess, request.callback));
        }
    }

    Vector3[] ConvertToWaypoints(Point start, Point end)
    {
        List<Point> path = new List<Point>();
        Point currentPoint = end;

        while (currentPoint != start)
        {
            path.Add(currentPoint);
            currentPoint = currentPoint.parent;
        }
        Vector3[] waypoints = ExtractPositionFromPoint(path);
        Array.Reverse(waypoints);
        return waypoints;
    }

    Vector3[] ExtractPositionFromPoint(List<Point> path)
    {
        List<Vector3> waypoints = new List<Vector3>();

        for (int i = 0; i < path.Count; i++)
        {
            waypoints.Add(path[i].worldPosition);
        }
        return waypoints.ToArray();
    }
}
