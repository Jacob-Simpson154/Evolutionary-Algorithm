using UnityEngine;

public class Heuristics : MonoBehaviour
{
    public int ManhattanDistance(Point a, Point b)
    {
        return 1 * Mathf.Abs((int)a.gridPos.x - (int)b.gridPos.x) + Mathf.Abs((int)a.gridPos.y - (int)b.gridPos.y);
    }

    public int EuclideanDistance(Point a, Point b)
    {
        return ((int)a.gridPos.x - (int)b.gridPos.x) * ((int)a.gridPos.x - (int)b.gridPos.x) + ((int)a.gridPos.y - (int)b.gridPos.y) * ((int)a.gridPos.y - (int)b.gridPos.y);
    }
}
