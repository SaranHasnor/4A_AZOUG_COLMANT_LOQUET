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
		if (robotNode.Attributes["team"] != null)
		{
			Team t = robotNode.Attributes["team"].Value == "1" ? Team.Player1 : Team.Player2;
			script.InitializeMapEntity(t, robotNode.Attributes["id"].Value);
		}
		else
			script.InitializeMapEntity(Team.None, robotNode.Attributes["id"].Value);
		script.InitializeRunnableEntity(ActionQueue.CreateFromXmlNode(robotNode));
		return script;
	}

	void OnMouseDown()
	{ // Have the current player select us and update the UI
		GameData.guiRenderer.SelectEntity(this);
	}
}
