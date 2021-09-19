using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Astar : MonoBehaviour
{
    AnimalManager manager;
    public void Init(AnimalManager m)
    {
        manager = m;
    }

    public void CreatePathToTarget(Vector3 start, Vector3 target)
    {
        PathManager.RequestPath(new PathRequest(start, target, PathCallback));
    }

    public void PathCallback(Vector3[] waypoints, bool pathSuccessful)
    {
        if (pathSuccessful)
        {
            Debug.Log("Found path");
            manager.movement_path.Clear();
            manager.movement_path.AddRange(waypoints);
        }
        else
        {
            Debug.Log("cant find path");
        }
    }
}
