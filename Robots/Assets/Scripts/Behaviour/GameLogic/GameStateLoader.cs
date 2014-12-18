using UnityEngine;
using System.IO;
using System.Xml;

public class GameStateLoader : MonoBehaviour
{
	[SerializeField]
	private TextAsset _template;

	void Start()
	{
		// Debug
		LoadTextAsset(_template);
	}

	public void LoadTextAsset(TextAsset asset)
	{
		var doc = new XmlDocument();
		doc.LoadXml(_template.text);

		GameState newState = GameState.CreateFromXmlDocument(doc);
		GameData.currentState = newState;
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
