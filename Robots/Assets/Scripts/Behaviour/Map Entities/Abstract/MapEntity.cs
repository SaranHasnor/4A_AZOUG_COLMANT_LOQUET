using UnityEngine;

public delegate void EntInteraction(EntityEvent actionType, params Object[] args);

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

	private static int _count;
	public static int count
	{ // Amount of MapEntities
		get
		{
			return _count;
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

		_id = _count++;
		player = -1;
	}

	public void Interact(EntityEvent action, params Object[] args)
	{
		if(OnEntityInteraction != null)
			OnEntityInteraction(action, args);
	}

	public int Move(Vector3 pos)
	{
		return GameData.currentState.map.MoveEntity(this, pos);
	}

	public void Destroy()
	{
		GameData.currentState.map.DeleteEntity(tr.position);
	}
}
