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

	public void LoadFromXmlDocument(XmlDocument doc)
	{
		GameData.currentState = GameState.CreateFromXmlDocument(doc);
		GameData.gameMaster.DidLoadMap();
	}

	public void LoadTextAsset(TextAsset asset)
	{
		var doc = new XmlDocument();
		doc.LoadXml(_template.text);

		this.LoadFromXmlDocument(doc);
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

		this.LoadFromXmlDocument(doc);
	}
}
