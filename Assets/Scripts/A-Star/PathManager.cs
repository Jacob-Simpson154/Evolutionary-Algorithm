﻿using UnityEngine;
using System.Collections.Generic;
using System;
using System.Threading;

public class PathManager : MonoBehaviour {

	Queue<PathResult> results = new Queue<PathResult>();

	static PathManager instance;
	ASTAR_Controller pathfinding;

	void Awake() {
		instance = this;
		pathfinding = GetComponent<ASTAR_Controller>();
	}

	void Update() {
		if (results.Count > 0) {
			int itemsInQueue = results.Count;
			lock (results) {
				for (int i = 0; i < itemsInQueue; i++) {
					PathResult result = results.Dequeue ();
					result.callback (result.path, result.success);
				}
			}
		}
	}

	public static void RequestPath(PathRequest request) {
		ThreadStart threadStart = delegate {
			instance.pathfinding.FindPath (request, instance.FinishedProcessingPath);
		};
		threadStart.Invoke ();
	}

	public void FinishedProcessingPath(PathResult result) {
		lock (results) {
			results.Enqueue (result);
		}
	}



}

//Pack the return data into a struct to pass back to caller efficiently
public struct PathResult {
	public Vector3[] path;
	public bool success;
	public Action<Vector3[], bool> callback;

	public PathResult (Vector3[] path, bool success, Action<Vector3[], bool> callback)
	{
		this.path = path;
		this.success = success;
		this.callback = callback;
	}

}

//Pack the return data into a struct to send to request path efficiently
public struct PathRequest {
	public Vector3 pathStart;
	public Vector3 pathEnd;
	public Action<Vector3[], bool> callback;

	public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3[], bool> _callback) {
		pathStart = _start;
		pathEnd = _end;
		callback = _callback;
	}

}