using System;
using UnityEngine;
using System.Collections.Generic;

public class Map
{
    private List<List<List<MapEntity>>> entities;
    public Map()
    {
        entities = new List<List<List<MapEntity>>>(32);
        for (var i = 0; i < entities.Capacity; ++i)
        {
            entities[i] = new List<List<MapEntity>>(32);
            for (var j = 0; j < entities.Capacity; ++j)
            {
                entities[i][j] = new List<MapEntity>(32);
            }
        }
    }
    private const float size = 5f;

    public void AddEntity(GameObject go, Vector3 pos)
    {
        var index = GetIndex(pos);
        entities[index.x][index.y][index.z] = go.GetComponent<MapEntity>();
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
        Vector3i index = GetIndex(pos);
        var me = new List<MapEntity>{ null, null, null, null, null, null };
        uint j = 0;
        for(var i = 0 ; i < entities.Count || j < 5 ; i++)
        {
            if(IsOnBlock(pos - new Vector3(pos.x - size, pos.y, pos.z))) //left
            {
                me[0] = entities[index.x][index.y][index.z];
                ++j;
            }
            else if(IsOnBlock(pos - new Vector3(pos.x + size, pos.y, pos.z))) //right
            {
                me[1] = entities[index.x][index.y][index.z];
                ++j;
            }
            else if(IsOnBlock(pos - new Vector3(pos.x, pos.y + size, pos.z))) //up
            {
                me[2] = entities[index.x][index.y][index.z];
                ++j;
            }
            else if(IsOnBlock(pos - new Vector3(pos.x, pos.y - size, pos.z))) //down
            {
                me[3] = entities[index.x][index.y][index.z];
                ++j;
            }
            else if(IsOnBlock(pos - new Vector3(pos.x, pos.y, pos.z + size))) //face
            {
                me[4] = entities[index.x][index.y][index.z];
                ++j;
            }
            else if(IsOnBlock(pos - new Vector3(pos.x, pos.y, pos.z - size))) //back
            {
                me[5] = entities[index.x][index.y][index.z];
                ++j;
            }
        }
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
    public bool IsOnBlock(Vector3 pos)
    {
        Vector3i index = GetIndex(pos);
        var curBlock = entities[index.x][index.y][index.z].gameObject.transform;
        return curBlock.position == pos;
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

 
}