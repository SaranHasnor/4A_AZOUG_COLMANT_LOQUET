using UnityEngine;

public class MapPosition 
{
	public MapPosition(int x, int y, int z)
	{
		_x = x;
		_y = y;
		_z = z;
	}

	public MapPosition(MapPosition pos)
	{
		_x = pos.x;
		_y = pos.y;
		_z = pos.z;
	}

	private int _x;
	public int x
	{
		get { return _x; }
		set { _x = value; }
	}
	private int _y;
	public int y
	{
		get { return _y; }
		set { _y = value; }
	}
	private int _z;
	public int z
	{
		get { return _z; }
		set { _z = value; }
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
#endregion

	private bool _Equals(MapPosition pos)
	{
		return (this.x == pos.x
			&& this.y == pos.y
			&& this.z == pos.z);
	}

	public override bool Equals(object obj)
	{
		if (obj == null)
			return false;

		return this._Equals((MapPosition)obj);
	}

	public override int GetHashCode()
	{
		return (new Vector3(this.x, this.y, this.z)).GetHashCode();
	}

	public static MapPosition FromString(string s)
	{
		var tmp = s.Split(',', '(', ')');
		return new MapPosition(int.Parse(tmp[1]), int.Parse(tmp[2]), int.Parse(tmp[3]));
	}

	public override string ToString()
	{
		return "(" + x + "," + y + "," + z + ")";
	}

	public static MapPosition zero
	{
		get
		{
			return new MapPosition(0, 0, 0);
		}
	}
}
