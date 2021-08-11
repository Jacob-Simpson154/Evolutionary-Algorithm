using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


//Sebastian Lague - https://github.com/SebLague/Field-of-View
[CustomEditor(typeof(FOV_Visualiser))]
public class FOV_Visualiser_Editor : Editor
{
	void OnSceneGUI()
	{
		FOV_Visualiser FOV = (FOV_Visualiser)target;

		Handles.color = Color.white;

		Handles.DrawWireArc(FOV.transform.position, Vector3.up, Vector3.forward, 360, FOV.GetViewAngle());

		Vector3 startAngle = FOV.DirFromAngle(-FOV.GetViewAngle() / 2, false);
		Vector3 finishAngle = FOV.DirFromAngle(FOV.GetViewAngle() / 2, false);

		Handles.DrawLine(FOV.transform.position, FOV.transform.position + startAngle * FOV.GetViewAngle());
		Handles.DrawLine(FOV.transform.position, FOV.transform.position + finishAngle * FOV.GetViewAngle());

		Handles.color = Color.red;

		foreach (ConsumableController visibleTarget in FOV.GetWater())
		{
			Handles.DrawLine(FOV.transform.position, visibleTarget.transform.position);
		}
	}
}
