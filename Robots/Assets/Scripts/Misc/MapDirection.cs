using UnityEngine;
using System.Collections;

public class MapDirection : MapPosition
{

	public MapDirection(int _x, int _y, int _z) : base(_x, _y, _z)
	{}

	public static MapDirection back
	{
		get
		{
			return new MapDirection(0, 0, -1);
		}
	}
	public static MapDirection forward
	{
		get
		{
			return new MapDirection(0, 0, 1);
		}
	}
	public static MapDirection down
	{
		get
		{
			return new MapDirection(0, -1, 0);
		}
	}
	public static MapDirection up
	{
		get
		{
			return new MapDirection(0, 1, 0);
		}
	}
	public static MapDirection left
	{
		get
		{
			return new MapDirection(-1, 0, 0);
		}
	}
	public static MapDirection right
	{
		get
		{
			return new MapDirection(1, 0, 0);
		}
	}

	/// <summary>
	///		Determines the direction between two position.
	/// </summary>
	/// <param name="startPos">This is the position from which the direction is determined.</param>
	/// <param name="targetPos">This is the position where the direction will point.</param>
	/// <returns>One direction of type <c>MapDirection</c>.</returns>
	public static MapDirection DirectionToMove(MapPosition startPos, MapPosition targetPos) {
		MapDirection direction;

		if (startPos.x != targetPos.x &&
			startPos.y == targetPos.y &&
			startPos.z == targetPos.z) {
			direction = targetPos.x > startPos.x ? right : left;
		} else if (startPos.x == targetPos.x &&
					startPos.y != targetPos.y &&
					startPos.z == targetPos.z) {
			direction = targetPos.y > startPos.y ? up : down;
		} else if (startPos.x == targetPos.x &&
					startPos.y == targetPos.y &&
					startPos.z != targetPos.z) {
			direction = targetPos.z > startPos.z ? forward : back;
		} else {
			Debug.LogWarning("Could not translate movement from " + startPos + " to " + targetPos);
			direction = new MapDirection(0, 0, 0);
		}

		return direction;
	}
}
