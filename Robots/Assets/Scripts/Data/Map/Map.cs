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

	public bool AddEntity(MapEntity me, MapPosition pos = null)
	{
		if (_entities.ContainsValue(me))
			return false;
		if (pos == null || !IsValidPosition(pos))
			return false;
		
		me.localPosition = pos;
		me.tr.position = ToWorldPos(pos);

		me.Interact(EntityEvent.Create, me);
		_entities[pos] = me;

		return true;
	}

	public void RemoveEntity(MapEntity me)
	{
		if (_entities.ContainsValue(me))
		{
			me.Interact(EntityEvent.Destroy, me);
			_entities[me.localPosition] = null;
		}
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
		return new MapPosition((int)(pos.x / blockSize), (int)(pos.y / blockSize), (int)(pos.z / blockSize));
	}

	public Vector3 ToWorldPos(MapPosition localPos)
	{
		return new Vector3(localPos.x * blockSize, localPos.y * blockSize, localPos.z * blockSize);
	}

	private bool IsValidPosition(MapPosition pos)
	{
		return (pos.x <= width && pos.y <= height && pos.z <= depth);
	}

	public bool TeleportEntity(MapEntity me, MapPosition pos)
	{
		if(!_entities.ContainsKey(pos) || GetEntity(pos) != null) return false;
		RemoveEntity(me);
		me.localPosition = pos;
		me.tr.position = ToWorldPos(pos);
		return true;
	}

	/// <summary>
	///		Allows to verify the possibility of moving an entity to a given position and
	///		returns the last possibly ateignable possition.
	/// </summary>
	/// <param name="entity">Is the entity that can be moved.</param>
	/// <param name="pos">This is the position that we want to achieve.</param>
	/// <returns>Returns a location on the map that the entity <c>entity</c> can achieve.</returns>
	public MapPosition CanMoveEntity(MapEntity entity, MapPosition pos) {
		var entLocalPos = ToLocalPos(entity.tr.position);
		var currentPos = ToLocalPos(entity.tr.position);

		if (entLocalPos.x != pos.x) {
			for (var i = entLocalPos.x; i < pos.x; i += _width) {
				var nextPos = new MapPosition(i, entLocalPos.y, entLocalPos.z);
				if (_entities.ContainsKey(nextPos) && GetEntity(ToWorldPos(nextPos)) == null)
					currentPos = nextPos;
				else
					break;
			}
		} else if (entLocalPos.y != pos.y) {
			for (var i = entLocalPos.y; i < pos.y; i += _height) {
				var nextPos = new MapPosition(entLocalPos.x, i, entLocalPos.z);
				if (_entities.ContainsKey(nextPos) && GetEntity(ToWorldPos(nextPos)) == null)
					currentPos = nextPos;
				else
					break;
			}
		} else if (entLocalPos.z != pos.z) {
			for (var i = entLocalPos.z; i < pos.z; i += _depth)
			{
				var nextPos = new MapPosition(entLocalPos.x, entLocalPos.y, i);
				if (_entities.ContainsKey(nextPos) && GetEntity(ToWorldPos(nextPos)) == null)
					currentPos = nextPos;
				else
					break;
			}
		}

		return currentPos;
	}

	public int MoveEntity(MapEntity me, MapPosition pos) {
		var entLocalPos = ToLocalPos(me.tr.position);
		var nextPos = CanMoveEntity(me, pos);

		if(nextPos.Equals(entLocalPos)) return -1;
		RemoveEntity(GetEntity(entLocalPos));
		me.localPosition = nextPos;
		me.transform.Translate(ToWorldPos(nextPos));
		me.Interact(EntityEvent.Move, me);
		return 0;
	}
}