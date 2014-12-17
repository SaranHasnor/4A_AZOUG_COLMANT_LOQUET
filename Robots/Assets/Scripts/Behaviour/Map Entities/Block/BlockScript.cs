using System.Runtime.CompilerServices;
using System.Xml;
using UnityEngine;

public class BlockScript : MapEntity {
	// The only argument for making this a runnable entity is to update their state each turn
	// Maybe we could also have blocks take actions at specific turns, but I'd like this to be a property instead

	void Start() {
		base.Initialize();
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

	void OnCollisionExit(Collision collisionInfo) {
		// TODO : ideally would require that properties are initialized with their related entity
		transform.parent.GetComponent<MapEntity>().Interact(EntityEvent.CollisionExit,
															collisionInfo.gameObject.GetComponent<MapEntity>());
	}

	public static MapEntity CreateFromXmlNode(XmlNode node)
	{
		/*this.gameObject = */ Instantiate(GameData.blockLibrary.blocks[node.ChildNodes[0].Attributes["type"].Value]);
		//id = node.ChildNodes[0].Attributes != null ? node.ChildNodes[0].Attributes["id"].Value : null;
		return null;
	}
}
