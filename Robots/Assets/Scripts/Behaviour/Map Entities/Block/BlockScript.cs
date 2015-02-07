using UnityEngine;
using System.Xml;
using System.Collections.Generic;

public class BlockScript : MapEntity
{
	public static BlockScript CreateFromXmlNode(XmlNode node)
	{ // TODO: Move some of this to the instantiate manager
		GameObject block = (GameObject)GameObject.Instantiate(GameData.instantiateManager.BlockPrefabForType(node.Attributes["type"].Value)
																, Map.GetWorldPos(Vector3i.FromString(node.Attributes["position"].Value))
																, Quaternion.identity);
		BlockScript script = block.GetComponent<BlockScript>();
		
		script.InitializeMapEntity(node.Attributes["id"].Value);
		
		if (node.Attributes["team"] != null)
		{
			script.team = MapEntity.StringToTeam(node.Attributes["team"].Value);
		}
		
		// Read any property arguments
		foreach (XmlNode propertyNode in node.ChildNodes)
		{
			foreach (EntProperty property in script.properties)
			{
				if (property.GetType().ToString() == propertyNode.Attributes["class"].Value)
				{
					Dictionary<string, string> parameters = new Dictionary<string, string>();

					foreach (string param in propertyNode.Attributes["params"].Value.Split(';'))
					{
						string[] str = param.Split('=');

						parameters.Add(str[0], str[1]);
					}

					property.SetParameters(parameters);
				}
			}
		}

		return script;
	}

	void OnCollisionEnter(Collision collision)
	{
		// TODO : ideally would require that properties are initialized with their related entity
		transform.parent.GetComponent<MapEntity>().Interact(EntityEvent.CollisionEnter,
															collision.gameObject.GetComponent<MapEntity>());
	}

	void OnCollisionStay(Collision collisionInfo)
	{
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
