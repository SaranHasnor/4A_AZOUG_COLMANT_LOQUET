using UnityEngine;
using System.Collections;
using System.Xml;

public class RobotScript : RunnableEntity
{
	void Start()
	{
		base.Initialize();
	}

	void OnMouseDown()
	{ // Have the current player select us and update the UI
		GameData.guiRenderer.SelectEntity(this);
	}

	public static MapEntity CreateFromXMLNode(XmlNode node)
	{
		/*this.gameObject = */
		Instantiate(GameData.blockLibrary.blocks[node.ChildNodes[0].Attributes["type"].Value]);
		//id = node.ChildNodes[0].Attributes != null ? node.ChildNodes[0].Attributes["id"].Value : null;
		return null;
	}
}
