using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class Pathfinding
{
	private static Dictionary<uint, WayPointScript> nodeTable = new Dictionary<uint, WayPointScript>();

	public static uint AddNode(WayPointScript script)
	{
		uint id = GetNumNodes();
		nodeTable.Add(id, script);
		return id;
	}

	public static uint GetNumNodes()
	{ // Convinience function
		return (uint)nodeTable.Count;
	}

	public static Vector3 GetNodePos(uint index)
	{
		if (index >= GetNumNodes())
		{
			return Vector3.zero;
		}

		return nodeTable[index].GetPos();
	}

	private static uint GetClosestWayPoint(Vector3 pos)
	{ // Returns the closest visible waypoint to the position NOTE: If none was found, returns nodeTable.Count
		uint tableLength = GetNumNodes();
		uint curIndex = tableLength;
		float curDist = -1;

		for (uint i = 0; i < tableLength; i++)
		{
			float dist = Vector3.Distance(pos, nodeTable[i].GetPos());

			if (dist < curDist || curDist == -1)
			{ // TODO: Add visibility check
				curIndex = i;
				curDist = dist;
			}
		}

		return curIndex;
	}

	private static float HeuristicCost(uint startNode, uint endNode)
	{ // Returns the minimal distance between two nodes (mostly a convinience function)
		return Vector3.Distance(GetNodePos(startNode), GetNodePos(endNode));
	}

	private static List<uint> BuildPath(Dictionary<uint, uint> previousNodeMap, uint currentNode)
	{
		List<uint> res = new List<uint>();

		if (previousNodeMap.ContainsKey(currentNode))
		{
			res.AddRange(BuildPath(previousNodeMap, previousNodeMap[currentNode]));
			res.Add(currentNode);
			return res;
		}

		res.Add(currentNode);
		return res;
	}

	private static uint GetKeyOfMinValue(List<uint> keyList, Dictionary<uint, float> values)
	{
		bool firstRun = true;
		float minValue = 0;
		uint minKey = 0;

		foreach (uint key in keyList)
		{
			if (firstRun || values[key] < minValue)
			{
				minValue = values[key];
				minKey = key;
				firstRun = false;
			}
		}

		return minKey;
	}

	private static List<uint> AStar(uint startNode, uint endNode)
	{ // A* goes here
		List<uint> closedSet = new List<uint>();
		List<uint> openSet = new List<uint>();
		Dictionary<uint, uint> previousNodeMap = new Dictionary<uint, uint>();

		openSet.Add(startNode);

		Dictionary<uint, float> goalScore = new Dictionary<uint, float>();
		Dictionary<uint, float> tempScore = new Dictionary<uint, float>();

		goalScore.Add(startNode, 0);

		tempScore.Add(startNode, HeuristicCost(startNode, endNode));

		while (openSet.Count > 0)
		{
			uint curNode = GetKeyOfMinValue(openSet, tempScore);

			if (curNode == endNode)
			{ // We're done!
				return BuildPath(previousNodeMap, endNode);
			}

			openSet.Remove(curNode);
			closedSet.Add(curNode);

			foreach (uint neighbour in nodeTable[curNode].GetNeighbours())
			{
				if (closedSet.Contains(neighbour))
				{
					continue;
				}

				float testGoalScore = goalScore[curNode] + HeuristicCost(curNode, neighbour);

				if (!openSet.Contains(neighbour) || testGoalScore < goalScore[neighbour])
				{
					if (previousNodeMap.ContainsKey(neighbour))
					{
						previousNodeMap.Remove(neighbour);
					}
					previousNodeMap.Add(neighbour, curNode);

					goalScore[neighbour] = testGoalScore;
					tempScore[neighbour] = goalScore[neighbour] + HeuristicCost(neighbour, endNode);

					if (!openSet.Contains(neighbour))
					{
						openSet.Add(neighbour);
					}
				}
			}
		}

		// You failed. Too bad.
		return new List<uint>();
	}

	public static List<uint> GetPath(Vector3 start, Vector3 end)
	{ // Returns the list of waypoints to follow to get from "start" to "end"
		uint startNode = GetClosestWayPoint(start);
		uint endNode = GetClosestWayPoint(end);

		if (startNode >= GetNumNodes() || endNode >= GetNumNodes())
		{ // ERROR: Origin or destination point has no visible waypoint
			return new List<uint>();
		}

		return AStar(startNode, endNode);
	}
}
