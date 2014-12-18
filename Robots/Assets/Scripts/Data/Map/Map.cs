using System;
using UnityEngine;
using System.Collections.Generic;

public class Map
{
	private List<List<List<MapEntity>>> _entities;
	public const int _limit = 32; //TODO : from xml
	public const float _size = 1f;
	public Map()
	{
		_entities = new List<List<List<MapEntity>>>(_limit);
		for(var i = 0 ; i < _entities.Capacity ; ++i)
		{
			_entities.Add(new List<List<MapEntity>>(_limit));
			for(var j = 0 ; j < _entities.Capacity ; ++j)
			{
				_entities[i].Add(new List<MapEntity>(_limit));
				for(var k = 0 ; k < _entities.Capacity ; ++k)
				{
					_entities[i][j].Add(null);

				}
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
		if(index.x > _limit || index.x < 0 || index.y > _limit || index.y < 0 || index.z > _limit || index.z < 0)
			return -1;
		else
			return 0;
	}

	public int TeleportEntity(MapEntity me, Vector3 pos)
	{
		if(IsOnLimit(GetLocalPos(pos)) == 0 && GetEntity(pos) == null)
		{
			DeleteEntity(me.tr.position);
			SetEntity(me, pos);
			me.tr.position = pos;
			return 0;
		}
		else
			return -1;
	}
	public int TeleportEntity(MapEntity me, Vector3i pos)
	{
		if(IsOnLimit(pos) == 0 && GetEntity(GetWorldPos(pos)) == null)
		{
			DeleteEntity(me.tr.position);
			SetEntity(me, GetWorldPos(pos));
			me.tr.position = GetWorldPos(pos);
			return 0;
		}
		else
			return -1;
	}

	public int MoveEntity(MapEntity me, Vector3i pos)
	{
		var entLocalPos = GetLocalPos(me.tr.position);
		var currentPos = GetLocalPos(me.tr.position);
		if(entLocalPos.x != pos.x)
		{
			for(var i = entLocalPos.x ; i < pos.x ; i += (int)_size)
			{
				var nextPos = new Vector3i(i, entLocalPos.y, entLocalPos.z);
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
				var nextPos = new Vector3i(entLocalPos.x, i, entLocalPos.z);
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
				var nextPos = new Vector3i(entLocalPos.x, entLocalPos.y, i);
				if(IsOnLimit(nextPos) == 0 && GetEntity(GetWorldPos(nextPos)) == null)
					currentPos = nextPos;
				else
					break;
			}
		}
		if(currentPos != entLocalPos)
		{
			DeleteEntity(GetWorldPos(entLocalPos));
			SetEntity(me, GetWorldPos(currentPos));
			me.transform.Translate(GetWorldPos(currentPos));
			return 0;
		}
		else
			return -1;

	}
}