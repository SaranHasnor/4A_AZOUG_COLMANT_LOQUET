using UnityEngine;
using System.Collections.Generic;
using System.Xml;

public class GameState
{ // Represents a snapshot of the game at a given time
	private Map _map;
	private Dictionary<string, MapEntity> _entities;
	private Dictionary<string, ActionQueue> _actions;

	public Map map
	{
		get
		{
			return _map;
		}
	}
	public Dictionary<string, MapEntity> entities
	{
		get
		{
			return new Dictionary<string, MapEntity>(_entities);
		}
	}
	public Dictionary<string, ActionQueue> actions
	{
		get
		{
			return new Dictionary<string, ActionQueue>(_actions);
		}
	}

	public GameState()
	{
		_map = new Map();
		_entities = new Dictionary<string, MapEntity>();
		_actions = new Dictionary<string, ActionQueue>();
	}

	public void UpdateWithPlayerGameState(GameState state, Team playerTeam)
	{ // Update the actions of entities owned by this player

	}

	public static GameState CreateFromXmlDocument(XmlDocument doc)
	{
		GameState newState = new GameState();

		XmlNode mapNode = doc.DocumentElement.SelectSingleNode("/gamestate/map");
		var width = mapNode.Attributes["width"].Value;
		var height = mapNode.Attributes["height"].Value;
		var depth = mapNode.Attributes["depth"].Value;

		foreach (XmlNode entityNode in mapNode.ChildNodes)
		{
			MapEntity newEntity;

			if (entityNode.Name == "block")
			{
				newEntity = BlockScript.CreateFromXmlNode(entityNode);
			}
			else if (entityNode.Name == "robot")
			{
				newEntity = RobotScript.CreateFromXmlNode(entityNode);
			}
			else
			{
				throw new System.Exception("aaaaaaaah");
			}

			newState._map.SetEntity(newEntity, newEntity.tr.position);
			newState._entities.Add(newEntity.id, newEntity);
		}

		XmlNode actionQueuesNode = doc.DocumentElement.SelectSingleNode("/gamestate/actions");

		foreach (XmlNode actionQueueNode in actionQueuesNode.ChildNodes)
		{
			ActionQueue newActionQueue = ActionQueue.CreateFromXmlNode(actionQueueNode); ;

			newState._actions.Add(actionQueueNode.Attributes["id"].Value, newActionQueue);
		}

		return newState;
	}

	public XmlDocument ToXml()
	{
		throw new System.NotImplementedException();
	}
}
