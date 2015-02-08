using UnityEngine;
using System.Xml;

public class RobotScript : RunnableEntity
{
	public static RobotScript CreateFromXmlNode(XmlNode node)
	{
		string id;
		Team team;
		MapPosition position;

		try
		{
			id = node.Attributes["id"].Value;
			position = MapPosition.FromString(node.Attributes["position"].Value);
		}
		catch (System.Exception e)
		{
			Debug.LogError("Mandatory parameter missing in Robot node");
			throw e;
		}

		team = (node.Attributes["team"] != null) ? MapEntity.StringToTeam(node.Attributes["team"].Value) : Team.None;

		RobotScript script = GameData.instantiateManager.SpawnRobot(id, position, team);

		return script;
	}

	void OnMouseDown()
	{ // Have the current player select us and update the UI
		GameData.guiRenderer.SelectEntity(this);
	}
}
