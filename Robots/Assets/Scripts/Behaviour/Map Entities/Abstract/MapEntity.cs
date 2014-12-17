using UnityEngine;
using System.Collections.Generic;

public delegate void EntInteraction(EntityEvent actionType, MapEntity entity);

public abstract class MapEntity : MonoBehaviour
{ // Describes an entity that has a physical presence on the map

	[SerializeField]
	private GameObject _logic; // Entity containing the properties

	[SerializeField]
	private Transform _transform;
	public Transform tr
	{
		get
		{
			return _transform;
		}
	}

	private EntProperty[] _properties;
	public event EntInteraction OnEntityInteraction;

	private static List<MapEntity> _entities = new List<MapEntity>(); // List of all map entities
	public static List<MapEntity> entities
	{
		get
		{
			return new List<MapEntity>(_entities);
		}
	}


	private int _id;
	public int id
	{
		get
		{
			return _id;
		}
	}

	public int player; // Player who controls this entity, none if < 0

	protected virtual void Initialize()
	{
		_properties = _logic.GetComponents<EntProperty>();
		foreach(var property in _properties)
		{
			property.AddListener(this);
		}

		_id = _entities.Count;
		_entities.Add(this);
		player = -1;
	}

	public void Interact(EntityEvent action, MapEntity entity)
	{
		if(OnEntityInteraction != null)
			OnEntityInteraction(action, entity);
	}

	public int Move(Vector3 pos)
	{
		return GameData.currentState.map.MoveEntity(this, pos);
	}

	public int Teleport(Vector3 pos)
	{
		return GameData.currentState.map.TeleportEntity(this, pos);
	}

	public void Destroy()
	{
		GameData.currentState.map.DeleteEntity(tr.position);
	}
}
