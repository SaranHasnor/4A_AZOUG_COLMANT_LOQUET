using UnityEngine;
using System.Collections;

public class MapPosition 
{
	public MapPosition(int _x, int _y, int _z)
	{
		x = _x;
		y = _y;
		z = _z;
	}

	public MapPosition(MapPosition pos)
	{
		x = pos.x;
		y = pos.y;
		z = pos.z;
	}

	public int x
	{
		get { return x; }
		set { x = value; }
	}
	public int y
	{
		get { return y; }
		set { y = value; }
	}
	public int z
	{
		get { return z; }
		set { z = value; }
	}

#region operators
	public static MapPosition operator +(MapPosition pos, MapPosition pos2)
	{
		return new MapPosition(pos.x + pos2.x, pos.y + pos2.y, pos.z + pos2.z);
	}
	public static MapPosition operator -(MapPosition pos, MapPosition pos2)
	{
		return new MapPosition(pos.x - pos2.x, pos.y - pos2.y, pos.z - pos2.z);
	}
	public static MapPosition operator *(MapPosition pos, int number)
	{
		return new MapPosition(pos.x * number, pos.y * number, pos.z * number);
	}
	public static MapPosition operator /(MapPosition pos, int number)
	{
		return new MapPosition(pos.x / number, pos.y / number, pos.z / number);
	}
	public static bool operator ==(MapPosition pos, MapPosition pos2)
	{
		return (pos.x == pos2.x
					&& pos.y == pos2.y
					&& pos.z == pos2.z);
	}
	public static bool operator !=(MapPosition pos, MapPosition pos2)
	{
		return !(pos == pos2);
	}
#endregion

	public static MapPosition FromString(string s)
	{
		var tmp = s.Split(',', '(', ')');
		return new MapPosition(int.Parse(tmp[1]), int.Parse(tmp[2]), int.Parse(tmp[3]));
	}

	public override string ToString()
	{
		return "(" + x + "," + y + "," + z + ")";
	}
}
