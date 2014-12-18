using UnityEngine;
using System.Xml;

public class RobotScript : RunnableEntity
{
	public static RobotScript CreateFromXmlNode(XmlNode RobotNode, XmlNode actionNode)
	{
		GameObject robot = (GameObject)GameObject.Instantiate(	GameData.instantiateManager.robotPrefab
																, Map.GetWorldPos(Vector3i.FromString(RobotNode.Attributes["position"].Value))
																, Quaternion.identity);
		RobotScript script = robot.GetComponent<RobotScript>();
		if (RobotNode.Attributes["team"] != null)
		{
			Team t = RobotNode.Attributes["team"].Value == "1" ? Team.Player1 : Team.Player2;
			script.InitializeMapEntity(t, RobotNode.Attributes["id"].Value);
		}
		else
			script.InitializeMapEntity(Team.None, RobotNode.Attributes["id"].Value);
		script.InitializeRunnableEntity(ActionQueue.CreateFromXmlNode(actionNode));
		return script;
	}

	void OnMouseDown()
	{ // Have the current player select us and update the UI
		GameData.guiRenderer.SelectEntity(this);
	}
}
