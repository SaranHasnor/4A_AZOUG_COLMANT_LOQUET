using UnityEngine;
using System.Collections;

public static class Utils
{
	// Here: static functions for common and useful algorithms
	public static MapPosition ToLocalPos(Vector3 pos)
	{
		return GameData.currentState.map.ToLocalPos(pos);
	}

	public static Vector3 ToWorldPos(MapPosition pos)
	{
		return GameData.currentState.map.ToWorldPos(pos);
	}
}
