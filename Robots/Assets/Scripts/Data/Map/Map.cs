using System;
using UnityEngine;
using System.Collections.Generic;

public class Map
{
	private List<List<List<MapEntity>>> _entities;
	private const int _limit = 32; //TODO : from xml
	private const float _size = 5f;
	public Map()
	{
		_entities = new List<List<List<MapEntity>>>(_limit);
		for(var i = 0 ; i < _entities.Capacity ; ++i)
		{
			_entities.Add(new List<List<MapEntity>>(_limit));
			for(var j = 0 ; j < _entities.Capacity ; ++j)
			{
				_entities[i].Add(new List<MapEntity>(_limit));
			}
		}
	}


	public int SetEntity(GameObject go, Vector3 pos)
	{
		var index = GetLocalPos(pos);
		if(IsOnLimit(index) == 0)
		{
			_entities[index.x][index.y][index.z] = go.GetComponent<MapEntity>();
			return 0;
		}
		else
			return -1;
	}
	public int SetEntity(MapEntity me, Vector3 pos)
	{
		var index = GetLocalPos(pos);
		if(IsOnLimit(index) == 0)
		{
			_entities[index.x][index.y][index.z] = me;
			return 0;
		}
		else
			return -1;
	}
	public MapEntity GetEntity(Vector3 pos)
	{
		var index = GetLocalPos(pos);
		try
		{
			return _entities[index.x][index.y][index.z];
		}
		catch(Exception)
		{
			return null;
		}
	}
	public MapEntity GetEntity(MapEntity me)
	{
		var index = GetLocalPos(me.transform.position); //TODO : creer variable dans mapEntity ? 
		try
		{
			return _entities[index.x][index.y][index.z];
		}
		catch(Exception)
		{
			return null;
		}
	}
	public List<MapEntity> GetNeighbours(Vector3 pos)
	{
		var me = new List<MapEntity>(6)
        {
             GetNear(pos, Vector3i.left)
            ,GetNear(pos, Vector3i.right)
            ,GetNear(pos, Vector3i.up)
            ,GetNear(pos, Vector3i.down)
            ,GetNear(pos, Vector3i.forward)
            ,GetNear(pos, Vector3i.back)
        };
		return me;
	}
	public MapEntity GetNear(Vector3 pos, Vector3i dir)
	{
		Vector3i index = GetLocalPos(pos) + dir;
		try
		{
			return _entities[index.x][index.y][index.z];
		}
		catch(Exception)
		{
			return null;
		}

	}
	public void DeleteEntity(Vector3 pos)
	{
		var index = GetLocalPos(pos);
		_entities[index.x][index.y][index.z] = null; //delete l'object a l'exterieur ?!
	}
	public static Vector3i GetLocalPos(Vector3 pos)
	{
		return new Vector3i((int)(pos.x / _size), (int)(pos.y / _size), (int)(pos.z / _size));
	}

	public static Vector3 GetWorldPos(Vector3i localPos)
	{
		return new Vector3(localPos.x * _size, localPos.y * _size, localPos.z * _size);
	}
	public static int IsOnLimit(Vector3i index)
	{
		if(index.x > _limit || index.y > _limit || index.z > _limit)
			return -1;
		else
			return 0;
	}

	public int TeleportEntity(MapEntity me, Vector3 pos)
	{
		if(IsOnLimit(GetLocalPos(pos)) == 0 && GetEntity(pos) == null)
		{
			DeleteEntity(me.tr.position);
			SetEntity(me.gameObject, pos);
			return 0;
		}
		else
			return -1;
	}

	public int MoveEntity(MapEntity me, Vector3 pos)
	{
		var currentPos = me.tr.position;
		if (me.tr.position.x != pos.x)
		{
			for (var i = me.tr.position.x; i < pos.x; i += _size)
			{
				var nextPos = new Vector3(i, me.tr.position.y, me.tr.position.z);
				if (IsOnLimit(GetLocalPos(nextPos)) == 0 && GetEntity(nextPos) == null)
					currentPos = nextPos;
				else
					break;
			}
		}
		else if (me.tr.position.y != pos.y)
		{
			for (var i = (int) me.tr.position.y; i < (int) pos.y; i += (int) _size)
			{
				var nextPos = new Vector3(me.tr.position.x, i, me.tr.position.z);
				if (IsOnLimit(GetLocalPos(nextPos)) == 0 && GetEntity(nextPos) == null)
					currentPos = nextPos;
				else
					break;
			}
		}
		else if (me.tr.position.z != pos.z)
		{
			for (var i = (int) me.tr.position.z; i < (int) pos.z; i += (int) _size)
			{
				var nextPos = new Vector3(me.tr.position.x, me.tr.position.y, i);
				if (IsOnLimit(GetLocalPos(nextPos)) == 0 && GetEntity(nextPos) == null)
					currentPos = nextPos;
				else
					break;
			}
		}
		if(currentPos != me.tr.position)
		{
			DeleteEntity(me.tr.position);
			SetEntity(me.gameObject, currentPos);
			return 0;
		}
		else
			return -1;

	}
}