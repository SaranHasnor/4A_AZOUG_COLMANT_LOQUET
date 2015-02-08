using UnityEngine;
using System.Xml;

public class RobotScript : RunnableEntity
{
	public static RobotScript CreateFromXmlNode(XmlNode robotNode)
	{
		GameObject robot = (GameObject)GameObject.Instantiate(GameData.instantiateManager.robotPrefab
																, GameData.currentState.map.ToWorldPos(MapPosition.FromString(robotNode.Attributes["position"].Value))
																, Quaternion.identity);
		RobotScript script = robot.GetComponent<RobotScript>();

		script.InitializeMapEntity(robotNode.Attributes["id"].Value);

		if (robotNode.Attributes["team"] != null)
		{
			script.team = MapEntity.StringToTeam(robotNode.Attributes["team"].Value);
		}

		script.InitializeRunnableEntity();
		return script;
	}

	void OnMouseDown()
	{ // Have the current player select us and update the UI
		GameData.guiRenderer.SelectEntity(this);
	}
}
