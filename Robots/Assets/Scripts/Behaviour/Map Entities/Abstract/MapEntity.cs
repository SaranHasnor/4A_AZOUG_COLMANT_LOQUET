using System.Linq;
using System.Xml;
using UnityEngine;

public delegate void EntInteraction(EntityEvent actionType, MapEntity entity);

public enum Team {
	None,
	Player1,
	Player2
}

public abstract class MapEntity : MonoBehaviour { // Describes an entity that has a physical presence on the map

	[SerializeField]
	private GameObject _logic; // Entity containing the properties

	[SerializeField]
	private Transform _transform;
	public Transform tr {
		get {
			return _transform;
		}
	}

	private EntProperty[] _properties;
	public EntProperty[] properties {
		get {
			return _properties;
		}
	}


	public event EntInteraction OnEntityInteraction;

	private string _id;
	public string id {
		get {
			return _id;
		}
	}

	private MapPosition _localPosition;
	public MapPosition localPosition {
		get {
			return _localPosition;
		}
		set {
			_localPosition = value;
		}
	}

	private Team _team;
	public Team team {
		get {
			return _team;
		}
	}

	public static Team StringToTeam(string str) {
		if (str.Equals("1"))
			return Team.Player1;
		if (str.Equals("2"))
			return Team.Player2;

		return Team.None;
	}

	private void _LoadProperties() {
		if (_properties != null) {
			foreach (var property in _properties) {
				property.RemoveListener(this);
			}
		}

		_properties = _logic.GetComponents<EntProperty>();
		foreach (var property in _properties) {
			property.AddListener(this);
		}
	}

	public void InitializeMapEntity(string id, MapPosition position, Team team = Team.None) {
		_LoadProperties();

		_id = id;
		_team = team;
		_localPosition = position;
	}

	public void Interact(EntityEvent action, MapEntity entity) {
		if (OnEntityInteraction != null)
			OnEntityInteraction(action, entity);
	}

	public bool Move(MapPosition pos) {
		return GameData.currentState.map.MoveEntity(this, pos);
	}

	public bool Push(MapDirection direction, int distance) {
		return GameData.currentState.map.PushEntity(this, direction, distance) > 0;
	}

	public void Destroy() {
		GameData.currentState.map.RemoveEntity(GameData.currentState.map.GetEntity(tr.position));
	}

	public XmlNode Serialize(XmlDocument doc) {
		var block = doc.CreateElement("block");

		var id = doc.CreateAttribute("id");
		id.Value = _id;
		block.Attributes.Append(id);

		var type = doc.CreateAttribute("type");
		type.Value = _logic.ToString();
		block.Attributes.Append(type);

		var pos = doc.CreateAttribute("position");
		pos.Value = _localPosition.ToString();
		block.Attributes.Append(pos);

		if (_team != Team.None) {
			var team = doc.CreateAttribute("team");
			team.Value = _team.ToString();
			block.Attributes.Append(team);
		}

		foreach (var property in _logic.GetComponents<EntProperty>().Select(prop => prop.Serialize(doc)).Where(property => property != null))
			block.AppendChild(property);

		return block;
	}
}
