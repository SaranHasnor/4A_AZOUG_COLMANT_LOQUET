using System;
using UnityEngine;
using System.IO;
using System.Xml;
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
						RobotScript.CreateFromXmlNode(node.ChildNodes[i]);
					}
				}
			}
		}

		nodes = doc.DocumentElement.SelectNodes("/gamestate/actions/");
		if(nodes != null)
		{
			foreach(XmlNode node in nodes)
			{
				for(var i = 0 ; i < node.ChildNodes.Count ; ++i)
				{
					if(node.ChildNodes[i].Name == "queue")
					{
						ActionQueue.CreateFromXmlNode(node.ChildNodes[i]);
					}
				}
			}
		}
	}

	public static Vector3i getVectorFromString(string s)
	{
		return new Vector3i(int.Parse(s[1].ToString()), int.Parse(s[3].ToString()), int.Parse(s[5].ToString()));
	}
}
