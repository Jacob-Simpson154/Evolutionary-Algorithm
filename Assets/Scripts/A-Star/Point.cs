using UnityEngine;
using System.Collections;

public class Point : MonoBehaviour, IHeapItem<Point>
{

	public bool walkable;				//whether the ai can walk to this point
	public Vector3 worldPosition;		//actual world position
	public Vector2 gridPos;				//position in worlddata 2D grid

	public int gCost;					//backwards cost
	public int hCost;					//forward cost
	public Point parent;				//connect point
	int heapIndex;						//position in heap list

	public void SetPoint(bool _walkable, Vector3 _worldPos, int _gridX, int _gridY)	//Set data values
	{
		walkable = _walkable;
		worldPosition = _worldPos;
		gridPos.x = _gridX;
		gridPos.y = _gridY;
	}

	public int fCost					//sum total
	{
		get
		{
			return gCost + hCost;
		}
	}

	public int HeapIndex				
	{
		get								//Get position in heap
		{
			return heapIndex;
		}
		set								//Set new position in heap
		{
			heapIndex = value;
		}
	}

    public int CompareTo(Point nodeToCompare)
    {
        int compare = fCost.CompareTo(nodeToCompare.fCost);
        if (compare == 0)
        {
            compare = hCost.CompareTo(nodeToCompare.hCost);
        }
        return -compare;
    }
}
