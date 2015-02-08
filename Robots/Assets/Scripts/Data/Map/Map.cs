using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class Map
{
	private Dictionary<MapPosition, MapEntity> _entities;

	private float _blockSize;
	public float blockSize
	{
		get { return _blockSize; }
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
		get { return _height; }
		set { _height = value; }
	}

	private int _depth;
	public int depth
	{
		get { return _depth; }
		set { _depth = value; }
	}

	public Map(int width, int height, int depth, float blockSize)
	{
		_width = width;
		_height = height;
		_depth = depth;
		_blockSize = blockSize;

		_entities = new Dictionary<MapPosition, MapEntity>(width * height * depth);
	}

	public int AddEntity(MapEntity me, MapPosition pos = null)
	{
		var newPos = pos ?? me.localPosition;
		if (_entities.ContainsValue(me))
			return -1;
		if (pos == null || !IsValidPosition(newPos))
			return -1;
		me.Interact(EntityEvent.Create, me);
		_entities[newPos] = me;
		return 0;
	}

	public void RemoveEntity(MapPosition pos)
	{
		if (_entities.ContainsKey(pos))
		{
			_entities[pos].Interact(EntityEvent.Destroy, _entities[pos]);
			_entities[pos] = null;
		}
		//else
		//	throw new Exception("Entity doesn't exist in current context");
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
	public MapEntity GetNeighbour(MapEntity entity, MapDirection direction)
	{
		try
		{
			return _entities[(entity.localPosition + direction)];
		}
		catch(Exception)
		{
			return null;
		}
	}
	public Dictionary<MapPosition, MapEntity> GetAllNeighbour(MapEntity entity)
	{
		var me = new Dictionary<MapPosition, MapEntity>(6);
		me[MapDirection.left]		= GetNeighbour(entity, MapDirection.left);
		me[MapDirection.right]		= GetNeighbour(entity, MapDirection.right);
		me[MapDirection.up]			= GetNeighbour(entity, MapDirection.up);
		me[MapDirection.down]		= GetNeighbour(entity, MapDirection.down);
		me[MapDirection.forward]	= GetNeighbour(entity, MapDirection.forward);
		me[MapDirection.back]		= GetNeighbour(entity, MapDirection.back);
		return me;
	}

	public MapPosition ToLocalPos(Vector3 pos)
	{
		return new MapPosition((int)(pos.x / _width), (int)(pos.y / _height), (int)(pos.z / _depth));
	}

	public Vector3 ToWorldPos(MapPosition localPos)
	{
		return new Vector3(localPos.x * _width, localPos.y * _height, localPos.z * _depth);
	}

	private bool IsValidPosition(MapPosition pos)
	{
		return (pos.x <= width && pos.y <= height && pos.z <= depth);
	}

	public int TeleportEntity(MapEntity me, MapPosition pos)
	{
		if(!_entities.ContainsKey(pos) || GetEntity(pos) != null) return -1;
		RemoveEntity(ToLocalPos(me.tr.position));
		AddEntity(me, pos);
		me.tr.position = ToWorldPos(pos);
		return 0;
	}

	public int MoveEntity(MapEntity me, MapPosition pos)
	{
		var entLocalPos = ToLocalPos(me.tr.position);
		var currentPos = ToLocalPos(me.tr.position);
		if(entLocalPos.x != pos.x)
		{
			for(var i = entLocalPos.x ; i < pos.x ; i += _width)
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
			for(var i = entLocalPos.y ; i < pos.y ; i += _height)
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
			for(var i = entLocalPos.z ; i < pos.z ; i += _depth)
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
		AddEntity(me, currentPos);
		me.transform.Translate(ToWorldPos(currentPos));
		me.Interact(EntityEvent.Move, me);
		return 0;
	}
}