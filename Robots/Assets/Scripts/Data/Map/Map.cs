using System;
using UnityEngine;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;

public class Map
{
	private Dictionary<MapPosition, MapEntity> _entities;

	private float _blockSize;
	public float blockSize
	{
		get{ return _blockSize; }
		set { _blockSize = value; }
	}

	private int _width;
	public int width
	{
		get { return _width; }
		set { _width = value; }
	}

	private int _height;
	public int height
	{
		get{ return _height; }
		set { _height = value; }
	}

	private int _depth;
	public int depth
	{
		get{ return _depth; }
		set { _depth = value; }
	}
	
	public Map(int width, int height, int depth, float blockSize)
	{
		_width = width;
		_height = height;
		_depth = depth;
		_blockSize = blockSize;

		_entities = new Dictionary<MapPosition, MapEntity>(width * height * depth);
		for(var i = 0 ; i < width ; ++i)
		{
			for(var j = 0 ; j < height ; ++j)
			{
				for(var k = 0 ; j < depth ; ++j)
				{
					_entities[new MapPosition(i, j, k)] = null;
				}
			}
		}
	}

	public int SetEntity(GameObject go, MapPosition pos)
	{
		if(!_entities.ContainsKey(pos)) return -1;
		_entities[pos] = go.GetComponent<MapEntity>();
		return 0;
	}
	public int SetEntity(MapEntity me, MapPosition pos)
	{
		if(!_entities.ContainsKey(pos)) return -1;
		_entities[pos] = me;
		return 0;
	}
	public MapEntity GetEntity(Vector3 pos)
	{
		return GetEntity(ToLocalPos(pos));
	}
	public MapEntity GetEntity(MapPosition pos)
	{
		try
		{
			return _entities[pos];
		}
		catch(Exception)
		{
			return null;
		}
	}
	public MapEntity GetEntity(MapEntity me)
	{
		try
		{
			return _entities[ToLocalPos(me.transform.position)];
		}
		catch(Exception)
		{
			return null;
		}
	}
	public List<MapEntity> GetNeighbours(MapPosition pos)
	{
		var me = new List<MapEntity>(6)
        {
             GetNear(pos, MapDirection.left)
            ,GetNear(pos, MapDirection.right)
            ,GetNear(pos, MapDirection.up)
            ,GetNear(pos, MapDirection.down)
            ,GetNear(pos, MapDirection.forward)
            ,GetNear(pos, MapDirection.back)
        };
		return me;
	}
	public MapEntity GetNear(MapPosition pos, MapDirection dir)
	{
		try
		{
			return _entities[new MapPosition(pos + dir)];
		}
		catch(Exception)
		{
			return null;
		}

	}
	public void RemoveEntity(MapPosition pos)
	{
		if(_entities.ContainsKey(pos)) //TODO : gerer le cas contraire ?
			_entities[pos] = null;
	}
	public MapPosition ToLocalPos(Vector3 pos)
	{
		return new MapPosition((int)(pos.x / _width), (int)(pos.y / _height), (int)(pos.z / _depth));
	}

	public Vector3 ToWorldPos(MapPosition localPos)
	{
		return new Vector3(localPos.x * _width, localPos.y * _height, localPos.z * _depth);
	}

	public int TeleportEntity(MapEntity me, MapPosition pos)
	{
		if (!_entities.ContainsKey(pos) || GetEntity(pos) != null) return -1;
		RemoveEntity(ToLocalPos(me.tr.position));
		SetEntity(me, pos);
		me.tr.position = ToWorldPos(pos);
		return 0;
	}

	public int MoveEntity(MapEntity me, MapPosition pos)
	{
		var entLocalPos = ToLocalPos(me.tr.position);
		var currentPos = ToLocalPos(me.tr.position);
		if(entLocalPos.x != pos.x) //TODO : adapter aux déplacements "complexes" ?! 
		{
			for(var i = entLocalPos.x ; i < pos.x ; i += (int)_width)
			{
				var nextPos = new MapPosition(i, entLocalPos.y, entLocalPos.z);
				if(_entities.ContainsKey(nextPos) && GetEntity(ToWorldPos(nextPos)) == null)
					currentPos = nextPos;
				else
					break;
			}
		}
		else if(entLocalPos.y != pos.y)
		{
			for(var i = entLocalPos.y ; i < pos.y ; i += (int)_height)
			{
				var nextPos = new MapPosition(entLocalPos.x, i, entLocalPos.z);
				if(_entities.ContainsKey(nextPos) && GetEntity(ToWorldPos(nextPos)) == null)
					currentPos = nextPos;
				else
					break;
			}
		}
		else if(entLocalPos.z != pos.z)
		{
			for(var i = entLocalPos.z ; i < pos.z ; i += (int)_depth)
			{
				var nextPos = new MapPosition(entLocalPos.x, entLocalPos.y, i);
				if(_entities.ContainsKey(nextPos) && GetEntity(ToWorldPos(nextPos)) == null)
					currentPos = nextPos;
				else
					break;
			}
		}
		if(currentPos == entLocalPos) return -1;
		RemoveEntity(entLocalPos);
		SetEntity(me, currentPos);
		me.transform.Translate(ToWorldPos(currentPos));
		return 0;
	}
}