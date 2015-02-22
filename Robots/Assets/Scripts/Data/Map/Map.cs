using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

public class Map {
	private Dictionary<MapPosition, MapEntity> _entities;

	private float _blockSize;
	public float blockSize {
		get { return _blockSize; }
		set { _blockSize = value; }
	}

	private int _width;
	public int width {
		get { return _width; }
		set { _width = value; }
	}

	private int _height;
	public int height {
		get { return _height; }
		set { _height = value; }
	}

	private int _depth;
	public int depth {
		get { return _depth; }
		set { _depth = value; }
	}

	public Map(int width, int height, int depth, float blockSize) {
		_width = width;
		_height = height;
		_depth = depth;
		_blockSize = blockSize;

		_entities = new Dictionary<MapPosition, MapEntity>(width * height * depth);
	}

	public bool AddEntity(MapEntity me, MapPosition pos = null) {
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

	public bool RemoveEntity(MapEntity me) {
		if (_entities.ContainsValue(me)) {
			me.Interact(EntityEvent.Destroy, me);
			_entities[me.localPosition] = null;
			return true;
		}
		return false;
	}

	public MapEntity GetEntity(Vector3 pos) {
		return GetEntity(ToLocalPos(pos));
	}
	public MapEntity GetEntity(MapPosition pos) {
		try {
			return _entities[pos];
		} catch (Exception) {
			return null;
		}
	}
	public MapEntity GetNeighbour(MapEntity entity, MapDirection direction) {
		try {
			return _entities[(entity.localPosition + direction)];
		} catch (Exception) {
			return null;
		}
	}
	public Dictionary<MapPosition, MapEntity> GetAllNeighbour(MapEntity entity) {
		var me = new Dictionary<MapPosition, MapEntity>(6);
		me[MapDirection.left] = GetNeighbour(entity, MapDirection.left);
		me[MapDirection.right] = GetNeighbour(entity, MapDirection.right);
		me[MapDirection.up] = GetNeighbour(entity, MapDirection.up);
		me[MapDirection.down] = GetNeighbour(entity, MapDirection.down);
		me[MapDirection.forward] = GetNeighbour(entity, MapDirection.forward);
		me[MapDirection.back] = GetNeighbour(entity, MapDirection.back);
		return me;
	}

	public MapPosition ToLocalPos(Vector3 pos) {
		return new MapPosition((int)(pos.x / blockSize), (int)(pos.y / blockSize), (int)(pos.z / blockSize));
	}

	public Vector3 ToWorldPos(MapPosition localPos) {
		return new Vector3(localPos.x * blockSize, localPos.y * blockSize, localPos.z * blockSize);
	}

	private bool IsValidPosition(MapPosition pos) {
		return (pos.x <= width && pos.y <= height && pos.z <= depth);
	}

	public bool MoveEntity(MapEntity me, MapPosition pos) {
		return MoveEntityAtPos(me.localPosition, pos);
	}

	public bool MoveEntityAtPos(MapPosition entityPos, MapPosition targetPos) {
		if (_entities.ContainsKey(targetPos)) {
			_entities[entityPos].Interact(EntityEvent.Collide, _entities[targetPos]);
			_entities[targetPos].Interact(EntityEvent.Collide, _entities[entityPos]);
			return false;
		}
		var entity = GetEntity(entityPos);
		entity.localPosition = targetPos;
		entity.tr.position = ToWorldPos(targetPos);
		_entities.Remove(entityPos);
		_entities.Add(targetPos, entity);
		entity.Interact(EntityEvent.Move, entity);
		return true;
	}

	public int PushEntity(MapEntity me, MapDirection direction, int distance) {
		return PushEntityAtPos(me.localPosition, direction, distance);
	}

	public int PushEntityAtPos(MapPosition entityPos, MapDirection direction, int distance) {
		var currentPos = entityPos;
		int i = 0;
		MapPosition obstacle = null;
		while (IsValidPosition(currentPos)) {
			if (distance > 0 && i == distance)
				break;
			if (_entities.ContainsKey(currentPos + direction)) {
				obstacle = currentPos + direction;
				break;
			}
			currentPos += direction;
			i++;
		}
		if (i > 0)
			MoveEntityAtPos(entityPos, currentPos);
		if (obstacle != null) {
			_entities[currentPos].Interact(EntityEvent.Collide, _entities[obstacle]);
			_entities[obstacle].Interact(EntityEvent.Collide, _entities[currentPos]);
		}
		return i;
	}

	public XmlElement Serialize(XmlDocument doc) {
		var element = doc.CreateElement("map");

		var width = doc.CreateAttribute("width");
		width.Value = _width.ToString();
		element.Attributes.Append(width);

		var height = doc.CreateAttribute("height");
		height.Value = _height.ToString();
		element.Attributes.Append(height);

		var depth = doc.CreateAttribute("depth");
		depth.Value = _depth.ToString();
		element.Attributes.Append(depth);

		foreach (var block in _entities.Select(entity => entity.Value.Serialize(doc)))
			element.AppendChild(block);

		return element;
	}
}