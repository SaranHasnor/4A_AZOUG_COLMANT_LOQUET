using UnityEngine;
using System.Collections.Generic;
using System.Xml;

public class GameState
{ // Represents a snapshot of the game at a given time
	private Map _map;
	private Dictionary<string, MapEntity> _entities;

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

	public GameState()
	{
		_map = new Map();
		_entities = new Dictionary<string, MapEntity>();
	}

	public void UpdateWithPlayerGameState(GameState state, Team playerTeam)
	{ // Update the actions of entities owned by this player

	}

	public static GameState CreateFromXmlDocument(XmlDocument doc)
	{
		GameState newState = new GameState();

		var nodes = doc.DocumentElement.SelectNodes("/gamestate/map");
		if (nodes != null)
		{
			foreach (XmlNode node in nodes)
			{
				var width = node.Attributes["width"].Value;
				var height = node.Attributes["height"].Value;
				var depth = node.Attributes["depth"].Value;
				for (var i = 0; i < node.ChildNodes.Count; ++i)
				{
					if (node.ChildNodes[i].Name == "block")
					{
						BlockScript newBlock = BlockScript.CreateFromXmlNode(node.ChildNodes[i]);
						newState.map.SetEntity(newBlock, newBlock.tr.position);
					}
					if (node.ChildNodes[i].Name == "robot")
					{
						var actionNodes = doc.DocumentElement.SelectNodes("/gamestate/actions/");
						if (actionNodes != null)
						{
							var j = 0;
							foreach (XmlNode actionNode in actionNodes)
							{
								if (actionNode.ChildNodes[j].Attributes["id"].Value == node.ChildNodes[i].Attributes["id"].Value)
								{
									RobotScript newRobot = RobotScript.CreateFromXmlNode(node.ChildNodes[i], actionNode.ChildNodes[j]);
									newState.map.SetEntity(newRobot, newRobot.tr.position);
								}
								++j;
							}
						}
					}
				}
			}
		}

		return newState;
	}

	public XmlDocument ToXml()
	{
		throw new System.NotImplementedException();
	}
}
