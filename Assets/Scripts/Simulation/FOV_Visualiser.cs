using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Sebastian Lague - https://github.com/SebLague/Field-of-View
public class FOV_Visualiser : MonoBehaviour
{
	public float GetViewAngle()
    {
		return GetComponent<AnimalBaseClass>().eyeSightAngle;
    }
	
	public float GetViewRange()
    {
		return GetComponent<AnimalBaseClass>().eyeSightRange;
    }

	public List<ConsumableController> GetWater()
    {
		return GetComponent<AnimalBaseClass>().visibleWater;
    }

	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
	{
		if (!angleIsGlobal)
		{
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}
}
