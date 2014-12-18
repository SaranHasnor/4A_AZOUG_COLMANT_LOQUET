using System.Xml;
using UnityEngine;

public class BlockScript : MapEntity {
	// The only argument for making this a runnable entity is to update their state each turn
	// Maybe we could also have blocks take actions at specific turns, but I'd like this to be a property instead

	public static BlockScript CreateFromXmlNode(XmlNode node)
	{
		GameObject block = (GameObject)GameObject.Instantiate(	GameData.instantiateManager.BlockPrefabForType(node.Attributes["type"].Value)
																, Map.GetWorldPos(Vector3i.FromString(node.Attributes["position"].Value))
																, Quaternion.identity);
		BlockScript script = block.GetComponent<BlockScript>();
		if(node.Attributes["team"] != null)
		{
			Team t = node.Attributes["team"].Value == "1" ? Team.Player1 : Team.Player2;
			script.InitializeMapEntity(t, node.Attributes["id"].Value);
		}
		else
			script.InitializeMapEntity(Team.None, node.Attributes["id"].Value);
		return script;
	}

	void OnCollisionEnter(Collision collision) {
		// TODO : ideally would require that properties are initialized with their related entity
		transform.parent.GetComponent<MapEntity>().Interact(EntityEvent.CollisionEnter,
															collision.gameObject.GetComponent<MapEntity>());
	}

	void OnCollisionStay(Collision collisionInfo) {
		// TODO : ideally would require that properties are initialized with their related entity
		transform.parent.GetComponent<MapEntity>().Interact(EntityEvent.CollisionStay,
															collisionInfo.gameObject.GetComponent<MapEntity>());
	}

	void OnCollisionExit(Collision collisionInfo)
	{
		// TODO : ideally would require that properties are initialized with their related entity
		transform.parent.GetComponent<MapEntity>().Interact(EntityEvent.CollisionExit,
															collisionInfo.gameObject.GetComponent<MapEntity>());
	}
}
