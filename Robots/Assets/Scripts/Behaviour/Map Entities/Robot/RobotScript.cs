using UnityEngine;
using System.Collections;
using System.Xml;

public class RobotScript : RunnableEntity
{
	public static RobotScript CreateFromXmlNode(XmlNode node)
	{
		GameObject robot = (GameObject)GameObject.Instantiate(GameData.instantiateManager.robotPrefab, Map.GetWorldPos(/*position*/Vector3i.forward), Quaternion.identity);
		RobotScript script = robot.GetComponent<RobotScript>();
		
		script.InitializeRunnableEntity(/*action queue parsed from the XML (this can be called by another function if needed)*/);
		script.InitializeMapEntity(/*stuff*/);

		return script;
	}

	void OnMouseDown()
	{ // Have the current player select us and update the UI
		GameData.guiRenderer.SelectEntity(this);
	}
}
