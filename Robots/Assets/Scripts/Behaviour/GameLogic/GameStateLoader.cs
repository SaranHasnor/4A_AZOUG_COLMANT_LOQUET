using System;
using UnityEngine;
using System.IO;
using System.Xml;
using UnityEditor;
using UnityEditor.AnimatedValues;

public class GameStateLoader : MonoBehaviour
{

	[SerializeField]
	public GameObject blockPrefab;

	void Start()
	{
		ParseXML();
	}

	public void ParseXML()
	{
		const string filePath = @"..\..\..\templateformat.xml";
		if(!File.Exists(filePath))
			Debug.LogError("Error : filePath not correct");
		var doc = new XmlDocument();
		doc.Load(filePath);
		var nodes = doc.DocumentElement.SelectNodes("/gamestate/map");
		if(nodes != null)
		{
			foreach(XmlNode node in nodes)
			{
				var width	= node.Attributes["width"].Value;
				var height	= node.Attributes["height"].Value;
				var depth	= node.Attributes["depth"].Value;
				for(var i = 0 ; i < node.ChildNodes.Count ; ++i)
				{
					if (node.ChildNodes[i].Name == "block")
					{
						BlockScript.CreateFromXmlNode(node.ChildNodes[i]);
					}
					if(node.ChildNodes[i].Name == "robot")
					{
						var actionNodes = doc.DocumentElement.SelectNodes("/gamestate/actions/");
						if(actionNodes != null)
						{
							var j = 0;
							foreach (XmlNode actionNode in actionNodes)
							{
								if (actionNode.ChildNodes[j].Attributes["id"].Value == node.ChildNodes[i].Attributes["id"].Value)
								{
									RobotScript.CreateFromXmlNode(node.ChildNodes[i], actionNode.ChildNodes[j]);
								}
								++j;
							}

						}
					}
				}
			}
		}

		
	}
}
