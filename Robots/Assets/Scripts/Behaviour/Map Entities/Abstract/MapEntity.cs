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
	public EntProperty[] properties
	{
		get
		{
			return _properties;
		}
	}


	public event EntInteraction OnEntityInteraction;

	private string _id;
	public string id
	{
		get
		{
			return _id;
		}
	}

	private MapPosition _localPosition;
	public MapPosition localPosition
	{
		get
		{
			return _localPosition;
		}
		set
		{
			_localPosition = value;
		}
	}

	private Team _team;
	public Team team
	{
		get
		{
			return _team;
		}
		set
		{
			if (_team == Team.None)
			{ // You can join a team if you don't have any, but you can't switch team afterwards
				_team = value;
			}
		}
	}

	public static Team StringToTeam(string str)
	{
		if (str.Equals("1"))
			return Team.Player1;
		if (str.Equals("2"))
			return Team.Player2;

		return Team.None;
	}

	protected void InitializeMapEntity(string id = null)
	{
		_properties = _logic.GetComponents<EntProperty>();
		foreach(var property in _properties)
		{
			property.AddListener(this);
		}

		_id = id ?? GameData.currentState.entities.Count.ToString();
	}

	public void Interact(EntityEvent action, MapEntity entity)
	{
		if(OnEntityInteraction != null)
			OnEntityInteraction(action, entity);
	}

	/// <summary>
	///		Allows to verify the possibility of moving this entity to a given position and
	///		returns the last possibly ateignable possition.
	/// </summary>
	/// <param name="pos">Is the entity that can be moved.</param>
	/// <returns>Returns a location on the map that this entity can achieve.</returns>
	public MapPosition CanMove(MapPosition pos) {
		return GameData.currentState.map.CanMoveEntity(this, pos);
	}

	public int Move(MapPosition pos)
	{
		return GameData.currentState.map.MoveEntity(this, pos);
	}

	public bool Teleport(MapPosition pos)
	{
		return GameData.currentState.map.TeleportEntity(this, pos);
	}

	public void Destroy()
	{
		GameData.currentState.map.RemoveEntity(GameData.currentState.map.GetEntity(tr.position));
	}
}
