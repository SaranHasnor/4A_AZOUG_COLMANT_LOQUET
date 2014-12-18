using UnityEngine;
using System.IO;
using System.Xml;

public class GameStateLoader : MonoBehaviour
{
	void Start()
	{
		// Debug
		LoadFile("../Workspace/template.xml");
	}

	public void LoadFile(string filePath)
	{
		if (!File.Exists(filePath))
		{
			Debug.LogError("Error : filePath not correct");
			return;
		}

		var doc = new XmlDocument();
		doc.Load(filePath);

		GameState newState = GameState.CreateFromXmlDocument(doc);
		GameData.currentState = newState;
	}
}
