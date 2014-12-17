using UnityEngine;
using System.Collections;
using System.Xml;

public class RobotScript : RunnableEntity
{
	public static RobotScript createFromXMLNode(XmlNode node)
	{
		//GameData.instantiateManager.SpawnRobot(node.Attributes["position"], node.Attributes["
		//id = node.ChildNodes[0].Attributes != null ? node.ChildNodes[0].Attributes["id"].Value : null;

		// TODO: Run InitializeRunnableEntity
		return null;
	}

	void OnMouseDown()
	{ // Have the current player select us and update the UI
		GameData.guiRenderer.SelectEntity(this);
	}
}
