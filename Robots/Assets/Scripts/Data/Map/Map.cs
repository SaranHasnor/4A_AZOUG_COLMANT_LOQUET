using System;
using UnityEngine;
using System.Collections.Generic;

public class Map
{
	private List<List<List<MapEntity>>> entities;
	private int limit = 32;
	public Map()
	{
		entities = new List<List<List<MapEntity>>>(limit);
		for(var i = 0 ; i < entities.Capacity ; ++i)
		{
			entities.Add(new List<List<MapEntity>>(limit));
			for(var j = 0 ; j < entities.Capacity ; ++j)
			{
				entities[i].Add(new List<MapEntity>(limit));
			}
		}
	}
	private const float size = 5f;

	public int SetEntity(GameObject go, Vector3 pos)
	{
		var index = GetIndex(pos);
		if(IsOnLimit(index) == 0)
		{
			entities[index.x][index.y][index.z] = go.GetComponent<MapEntity>();
			return 0;
		}
		else
			return -1;
	}
	public int SetEntity(MapEntity me, Vector3 pos)
	{
		var index = GetIndex(pos);
		if(IsOnLimit(index) == 0)
		{
			entities[index.x][index.y][index.z] = me;
			return 0;
		}
		else
			return -1;
	}
	public MapEntity GetEntity(Vector3 pos)
	{
		var index = GetIndex(pos);
		try
		{
			return entities[index.x][index.y][index.z];
		}
		catch(Exception)
		{
			return null;
		}
	}
	public MapEntity GetEntity(MapEntity me)
	{
		var index = GetIndex(me.transform.position); //TODO : creer variable dans mapEntity ? 
		try
		{
			return entities[index.x][index.y][index.z];
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
		Vector3i index = GetIndex(pos) + dir;
		try
		{
			return entities[index.x][index.y][index.z];
		}
		catch(Exception)
		{
			return null;
		}

	}
	public void DeleteEntity(Vector3 pos)
	{
		var index = GetIndex(pos);
		entities[index.x][index.y][index.z] = null; //delete l'object a l'exterieur ?!
	}
	public Vector3i GetIndex(Vector3 pos)
	{
		return new Vector3i((int)(pos.x / size), (int)(pos.y / size), (int)(pos.z / size));
	}
	public int IsOnLimit(Vector3i index)
	{
		if(index.x > limit || index.y > limit || index.z > limit)
			return -1;
		else
			return 0;
	}

	public int TeleportEntity(MapEntity me, Vector3 pos)
	{
		if(IsOnLimit(GetIndex(pos)) == 0 && GetEntity(pos) == null)
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
			for (var i = me.tr.position.x; i < pos.x; i += size)
			{
				var nextPos = new Vector3(i, me.tr.position.y, me.tr.position.z);
				if (IsOnLimit(GetIndex(nextPos)) == 0 && GetEntity(nextPos) == null)
					currentPos = nextPos;
				else
					break;
			}
		}
		else if (me.tr.position.y != pos.y)
		{
			for (var i = (int) me.tr.position.y; i < (int) pos.y; i += (int) size)
			{
				var nextPos = new Vector3(me.tr.position.x, i, me.tr.position.z);
				if (IsOnLimit(GetIndex(nextPos)) == 0 && GetEntity(nextPos) == null)
					currentPos = nextPos;
				else
					break;
			}
		}
		else if (me.tr.position.z != pos.z)
		{
			for (var i = (int) me.tr.position.z; i < (int) pos.z; i += (int) size)
			{
				var nextPos = new Vector3(me.tr.position.x, me.tr.position.y, i);
				if (IsOnLimit(GetIndex(nextPos)) == 0 && GetEntity(nextPos) == null)
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