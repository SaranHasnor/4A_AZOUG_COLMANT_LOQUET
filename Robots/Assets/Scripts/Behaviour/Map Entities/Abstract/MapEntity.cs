using UnityEngine;
using System.Collections.Generic;

public delegate void EntInteraction(EntityEvent actionType, MapEntity entity);

public enum Team {
	None,
	Player1,
	Player2
}

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

	private string _id;
	public string id
	{
		get
		{
			return _id;
		}
	}

	private Team _team;
	public Team team
	{
		get
		{
			return _team;
		}
	}

	protected void InitializeMapEntity(Team team = Team.None, string id = null)
	{
		_properties = _logic.GetComponents<EntProperty>();
		foreach(var property in _properties)
		{
			property.AddListener(this);
		}

		_id = id ?? GameData.currentState.entities.Count.ToString();
		_team = team;
	}

	public void Interact(EntityEvent action, MapEntity entity)
	{
		if(OnEntityInteraction != null)
			OnEntityInteraction(action, entity);
	}

	public int Move(Vector3i pos)
	{
		return GameData.currentState.map.MoveEntity(this, pos);
	}

	public int Teleport(Vector3i pos)
	{
		return GameData.currentState.map.TeleportEntity(this, pos);
	}

	public void Destroy()
	{
		GameData.currentState.map.DeleteEntity(tr.position);
	}
}
