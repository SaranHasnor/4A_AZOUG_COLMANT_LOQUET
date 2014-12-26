﻿using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class Map
{
	private Dictionary<MapPosition, MapEntity> _entities;
	public const int _limit = 32; //TODO : from xml
	public const float _size = 1f;
	public Map()
	{
		_entities = new Dictionary<MapPosition, MapEntity>((int)Mathf.Pow(_size, 3));
		for (var i = 0; i < _size; ++i)
		{
			for(var j = 0 ; j < _size ; ++j)
			{
				for(var k = 0 ; j < _size ; ++j)
				{
					_entities[new MapPosition(i, j, k)] = null;
				}
			}
		}
	}

	public int SetEntity(GameObject go, MapPosition pos)
	{
		if(IsOnLimit(pos) == 0)
		{
			_entities[pos] = go.GetComponent<MapEntity>();
			return 0;
		}
		else
			return -1;
	}
	public int SetEntity(MapEntity me, MapPosition pos)
	{
		if(IsOnLimit(pos) == 0)
		{
			_entities[pos] = me;
			return 0;
		}
		else
			return -1;
	}
	public MapEntity GetEntity(Vector3 pos)
	{
		return GetEntity(GetLocalPos(pos));
	}
	public MapEntity GetEntity(MapPosition pos) {
		try {
			return _entities[pos];
		} catch (Exception) {
			return null;
		}
	}
	public MapEntity GetEntity(MapEntity me)
	{
		var index = GetLocalPos(me.transform.position); //TODO : creer variable dans mapEntity ? 
		try
		{
			return _entities[index];
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
		_entities[pos] = null;
	}
	public static MapPosition GetLocalPos(Vector3 pos)
	{
		return new MapPosition((int)(pos.x / _size), (int)(pos.y / _size), (int)(pos.z / _size));
	}

	public static Vector3 GetWorldPos(MapPosition localPos)
	{
		return new Vector3(localPos.x * _size, localPos.y * _size, localPos.z * _size);
	}
	public static int IsOnLimit(MapPosition index)
	{
		if(index.x > _limit || index.x < 0 || index.y > _limit || index.y < 0 || index.z > _limit || index.z < 0)
			return -1;
		return 0;
	}

	public int TeleportEntity(MapEntity me, MapPosition pos)
	{
		if(IsOnLimit(pos) == 0 && GetEntity(pos) == null)
		{
			RemoveEntity(GetLocalPos(me.tr.position));
			SetEntity(me, pos);
			me.tr.position = GetWorldPos(pos);
			return 0;
		}
		return -1;
	}

	public int MoveEntity(MapEntity me, MapPosition pos)
	{
		var entLocalPos = GetLocalPos(me.tr.position);
		var currentPos = GetLocalPos(me.tr.position);
		if(entLocalPos.x != pos.x)
		{
			for(var i = entLocalPos.x ; i < pos.x ; i += (int)_size)
			{
				var nextPos = new MapPosition(i, entLocalPos.y, entLocalPos.z);
				if(IsOnLimit(nextPos) == 0 && GetEntity(GetWorldPos(nextPos)) == null)
					currentPos = nextPos;
				else
					break;
			}
		}
		else if(entLocalPos.y != pos.y)
		{
			for(var i = entLocalPos.y ; i < pos.y ; i += (int)_size)
			{
				var nextPos = new MapPosition(entLocalPos.x, i, entLocalPos.z);
				if(IsOnLimit(nextPos) == 0 && GetEntity(GetWorldPos(nextPos)) == null)
					currentPos = nextPos;
				else
					break;
			}
		}
		else if(entLocalPos.z != pos.z)
		{
			for(var i = entLocalPos.z ; i < pos.z ; i += (int)_size)
			{
				var nextPos = new MapPosition(entLocalPos.x, entLocalPos.y, i);
				if(IsOnLimit(nextPos) == 0 && GetEntity(GetWorldPos(nextPos)) == null)
					currentPos = nextPos;
				else
					break;
			}
		}
		if(currentPos != entLocalPos)
		{
			RemoveEntity(entLocalPos);
			SetEntity(me, currentPos);
			me.transform.Translate(GetWorldPos(currentPos));
			return 0;
		}
		return -1;
	}
}