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
        for (var i = 0; i < entities.Capacity; ++i)
        {
            entities.Add(new List<List<MapEntity>>(limit));
            for (var j = 0; j < entities.Capacity; ++j)
            {
                entities[i].Add(new List<MapEntity>(limit));
            }
        }
    }
    private const float size = 5f;

    public int SetEntity(GameObject go, Vector3 pos)
    {
        var index = GetIndex(pos);
        if (IsOnLimit(index) == 0)
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
        if (IsOnLimit(index) == 0)
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
        catch (Exception)
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
        catch (Exception)
        {
            return null;
        }
        
    }
    public void DeleteEntity(Vector3 pos)
    {
        var index = GetIndex(pos);

        entities[index.x][index.y].Remove(entities[index.x][index.y][index.z]); //delete l'object a l'exterieur ?!
    }
    public Vector3i GetIndex(Vector3 pos)
    {
        return new Vector3i( (int) (pos.x / size),(int) (pos.y / size), (int) (pos.z / size) );
    }
    public int IsOnLimit(Vector3i index)
    {
        if (index.x > limit || index.y > limit || index.z > limit)
            return -1;
        else
            return 0;
    }

    public int MoveEntity(MapEntity me, Vector3 pos) //TODO : gerer que des pos?
    {
        if (IsOnLimit(GetIndex(pos)) == 0)
        {
            DeleteEntity(me.tr.position);
            SetEntity(me.go, pos);
            return 0;
        }
        else
            return -1;
    }
}