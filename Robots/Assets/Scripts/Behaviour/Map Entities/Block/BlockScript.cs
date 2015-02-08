using UnityEngine;
using System.Xml;
using System.Linq;

public class BlockScript : MapEntity
{
	public static BlockScript CreateFromXmlNode(XmlNode node)
	{
		string id;
		string type;
		Team team;
		MapPosition position;
		
		try {
			id = node.Attributes["id"].Value;
			type = node.Attributes["type"].Value;
			position = MapPosition.FromString(node.Attributes["position"].Value);
		} catch (System.Exception e) {
			Debug.LogError("Mandatory parameter missing in Block node");
			throw e;
		}

		team = (node.Attributes["team"] != null) ? MapEntity.StringToTeam(node.Attributes["team"].Value) : Team.None;

		BlockScript script = GameData.instantiateManager.SpawnBlock(type, id, position, team);
		
		// Read any property arguments
		foreach (XmlNode propertyNode in node.ChildNodes) {
			foreach (var property in script.properties) {
				if (property.GetType().ToString() == propertyNode.Attributes["class"].Value) {
					var parameters = propertyNode.Attributes["params"].Value.Split(';').Select(param => param.Split('=')).ToDictionary(str => str[0], str => str[1]);

					property.SetParameters(parameters);
				}
			}
		}

		return script;
	}
}

//public class EntPropertyDestroy : EntProperty {
//	protected override void _Interact(EntityEvent actionType, MapEntity entity) {
//		if (actionType == EntityEvent.Destroy) {
//			// TODO : Completer algo property Destroy
//			throw new System.ArgumentException(System.String.Format("L'event {0} a été levé mais l'implémentation n'est pas terminé.", actionType));
//		}
//	}
//}
