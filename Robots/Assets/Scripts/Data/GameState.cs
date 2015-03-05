using System.Collections.Generic;
using System.Xml;

public class GameState { // Represents a snapshot of the game at a given time
	private Map _map;
	public Map map {
		get {
			return _map;
		}
	}

	private Dictionary<string, MapEntity> _entities;
	public Dictionary<string, MapEntity> entities {
		get {
			return new Dictionary<string, MapEntity>(_entities);
		}
	}

	private Dictionary<string, ActionQueue> _actions;
	public Dictionary<string, ActionQueue> actions {
		get {
			return new Dictionary<string, ActionQueue>(_actions);
		}
	}

	public GameState() {
		_entities = new Dictionary<string, MapEntity>();
		_actions = new Dictionary<string, ActionQueue>();
	}

	public bool AddEntity(MapEntity entity, MapPosition position = null) {
		if (_entities.ContainsValue(entity)) {
			return false;
		}

		_entities.Add(entity.id, entity);

		return _map.AddEntity(entity, position);
	}

	public bool RemoveEntity(MapEntity entity) {
		if (!_entities.ContainsValue(entity)) {
			return false;
		}

		_entities.Remove(entity.id);

		return _map.RemoveEntity(entity);
	}

	public void UpdateWithPlayerGameState(GameState state, Team playerTeam) { // Update the actions of entities owned by this player

	}

	public static GameState CreateFromXmlDocument(XmlDocument doc) {
		GameState newState = new GameState();

		XmlNode mapNode = doc.DocumentElement.SelectSingleNode("/gamestate/map");

		int width = int.Parse(mapNode.Attributes["width"].Value);
		int height = int.Parse(mapNode.Attributes["height"].Value);
		int depth = int.Parse(mapNode.Attributes["depth"].Value);

		newState._map = new Map(width, height, depth, 1.0f);

		foreach (XmlNode entityNode in mapNode.ChildNodes) {
			MapEntity newEntity;

			if (entityNode.Name == "block") {
				newEntity = BlockScript.CreateFromXmlNode(entityNode);
			} else if (entityNode.Name == "robot") {
				newEntity = RobotScript.CreateFromXmlNode(entityNode);
			} else {
				throw new System.ArgumentException(System.String.Format("L'information inatendu {0}, c'est produit dans CreateFromXmlDocument.", entityNode.Name));
			}

			newState._entities.Add(newEntity.id, newEntity);
			newState._map.AddEntity(newEntity, newEntity.localPosition);
		}

		XmlNode actionQueuesNode = doc.DocumentElement.SelectSingleNode("/gamestate/actions");

		foreach (XmlNode actionQueueNode in actionQueuesNode.ChildNodes) {
			ActionQueue newActionQueue = ActionQueue.CreateFromXmlNode(actionQueueNode);

			newState._actions.Add(actionQueueNode.Attributes["id"].Value, newActionQueue);
		}

		return newState;
	}

	public XmlDocument ToXml() {
		throw new System.NotImplementedException();
	}


	public XmlDocument Serialize() {
		var doc = new XmlDocument();

		// <gamestate>
		var nodeGameState = doc.CreateElement("GameState");

		// <map>
		nodeGameState.AppendChild(_map.Serialize(doc));
		// </map>

		// <actions>
		var nodeActions = doc.CreateElement("actions");
		foreach (var action in _actions) {
			var entity = action.Value.Serialize(doc);

			var id = doc.CreateAttribute("id");
			id.Value = action.Key;
			if (entity.Attributes != null)
				entity.Attributes.Append(id);

			nodeActions.AppendChild(entity);
		}
		nodeGameState.AppendChild(nodeActions);
		// </actions>

		doc.AppendChild(nodeGameState);
		// </gamestate>
		return doc;
	}
}