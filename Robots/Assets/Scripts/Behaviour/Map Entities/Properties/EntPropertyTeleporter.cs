using UnityEngine;
using System.Collections.Generic;
using System.Xml;

public class EntPropertyTeleporter : EntProperty {
	[SerializeField]
	private string _targetID;

	[SerializeField]
	private uint _frequencyTeleport = 1;

	private static uint _sinceLastTeleport = 0;

	protected override void _Interact(EntityEvent action, MapEntity entity)
	{
		if (action == EntityEvent.Turn)
		{
			++_sinceLastTeleport;
		}
		else if (action == EntityEvent.Collide)
		{
			if (_sinceLastTeleport >= _frequencyTeleport)
			{
				GameData.currentState.entities[_targetID].Interact(EntityEvent.Teleport, entity);
				_sinceLastTeleport = 0;
			}
		}
	}

	public override void SetParameters(Dictionary<string, string> parameters)
	{
		if (parameters.ContainsKey("target"))
		{
			_targetID = parameters["target"];
		}
	}

	public override XmlNode Serialize(XmlDocument doc) {
		var property = doc.CreateElement("property");

		var c = doc.CreateAttribute("class");
		c.Value = ToString();
		property.Attributes.Append(c);

		var param = doc.CreateAttribute("params");
		param.Value = "target=" + _targetID;
		property.Attributes.Append(param);

		return property;
	}
}