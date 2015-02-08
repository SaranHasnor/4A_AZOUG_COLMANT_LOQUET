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


}
