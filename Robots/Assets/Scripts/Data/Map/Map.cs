using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Map
{
    public List<List<List<MapEntity>>> Blocks;
    public Map()
    {
        Blocks = new List<List<List<MapEntity>>>(32);
        for (var i = 0; i < Blocks.Capacity; ++i)
        {
            Blocks[i] = new List<List<MapEntity>>(32);
            for (var j = 0; j < Blocks.Capacity; ++j)
            {
                Blocks[i][j] = new List<MapEntity>(32);
            }
        }
    }
    private const float size = 5f;

    public void AddBlock(GameObject go, Vector3 pos)
    {
        var index = GetIndex(pos);
        Blocks[index.x][index.y][index.z] = go.GetComponent<MapEntity>();
    }

    public MapEntity GetBlock(Vector3 pos)
    {
        var index = GetIndex(pos);
        try
        {
            return Blocks[index.x][index.y][index.z];
        }
        catch (Exception)
        {
            return null;
        }
    }
    
    public List<MapEntity> GetNeighbours(Vector3 pos)
    {
        Vector3i index = GetIndex(pos);
        var me = new List<MapEntity>{ null, null, null, null, null, null };
        uint j = 0;
        for(var i = 0 ; i < Blocks.Count || j < 5 ; i++)
        {
            if(IsOnBlock(pos - new Vector3(pos.x - size, pos.y, pos.z))) //left
            {
                me[0] = Blocks[index.x][index.y][index.z];
                ++j;
            }
            else if(IsOnBlock(pos - new Vector3(pos.x + size, pos.y, pos.z))) //right
            {
                me[1] = Blocks[index.x][index.y][index.z];
                ++j;
            }
            else if(IsOnBlock(pos - new Vector3(pos.x, pos.y + size, pos.z))) //up
            {
                me[2] = Blocks[index.x][index.y][index.z];
                ++j;
            }
            else if(IsOnBlock(pos - new Vector3(pos.x, pos.y - size, pos.z))) //down
            {
                me[3] = Blocks[index.x][index.y][index.z];
                ++j;
            }
            else if(IsOnBlock(pos - new Vector3(pos.x, pos.y, pos.z + size))) //face
            {
                me[4] = Blocks[index.x][index.y][index.z];
                ++j;
            }
            else if(IsOnBlock(pos - new Vector3(pos.x, pos.y, pos.z - size))) //back
            {
                me[5] = Blocks[index.x][index.y][index.z];
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
            return Blocks[index.x][index.y][index.z];
        }
        catch (Exception)
        {
            return null;
        }
        
    }

    public bool IsOnBlock(Vector3 pos)
    {
        Vector3i index = GetIndex(pos);
        var curBlock = Blocks[index.x][index.y][index.z].gameObject.transform;
        return curBlock.position == pos;
    }
    public void DeleteBlock(Vector3 pos)
    {
        var index = GetIndex(pos);
        Blocks[index.x][index.y].Remove(Blocks[index.x][index.y][index.z]); //delete l'object a l'exterieur ?!
    }

    public Vector3i GetIndex(Vector3 pos)
    {
        return new Vector3i( (int) (pos.x / size),(int) (pos.y / size), (int) (pos.z / size) );
    }
}