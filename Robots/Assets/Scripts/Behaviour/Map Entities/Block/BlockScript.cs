using UnityEngine;
using System.Xml;
using System.Linq;

public class BlockScript : MapEntity {
	// The only argument for making this a runnable entity is to update their state each turn
	// Maybe we could also have blocks take actions at specific turns, but I'd like this to be a property instead

	public static BlockScript CreateFromXmlNode(XmlNode node) {
		GameObject block = (GameObject)GameObject.Instantiate(GameData.instantiateManager.BlockPrefabForType(node.Attributes["type"].Value)
																, GameData.currentState.map.ToWorldPos(MapPosition.FromString(node.Attributes["position"].Value))
																, Quaternion.identity);
		BlockScript script = block.GetComponent<BlockScript>();
		if (node.Attributes["team"] != null) {
			Team t = node.Attributes["team"].Value == "1" ? Team.Player1 : Team.Player2;
			script.InitializeMapEntity(t, node.Attributes["id"].Value);
		} else
			script.InitializeMapEntity(Team.None, node.Attributes["id"].Value);

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

public class EntPropertyDestroy : EntProperty {
	protected override void _Interact(EntityEvent actionType, MapEntity entity) {
		if (actionType == EntityEvent.Destroy) {
			// TODO : Completer algo property Destroy
			throw new System.ArgumentException(System.String.Format("L'event {0} a été levé mais l'implémentation n'est pas terminé.", actionType));
		}
	}
}