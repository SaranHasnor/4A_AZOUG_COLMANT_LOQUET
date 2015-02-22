﻿using UnityEngine;
using System.Collections.Generic;
﻿using System.Xml;

public class EntPropertyTeleporterTarget : EntProperty
{
	[SerializeField]
	private MapPosition _exitDirection = MapDirection.up;

	protected override void _Interact(EntityEvent actionType, MapEntity entity)
	{
		if (actionType == EntityEvent.Teleport)
		{
			entity.Move(owner.localPosition + _exitDirection);
		}
	}

	public override void SetParameters(Dictionary<string, string> parameters)
	{
		if (parameters.ContainsKey("exitDirection"))
		{
			_exitDirection = MapDirection.FromString(parameters["exitDirection"]);
		}
	}

	public override XmlNode Serialize(XmlDocument doc) {
		var property = doc.CreateElement("property");

		var c = doc.CreateAttribute("class");
		c.Value = ToString();
		property.Attributes.Append(c);

		var param = doc.CreateAttribute("params");
		param.Value = "exitDirection=" + _exitDirection.ToString();
		property.Attributes.Append(param);

		return property;
	}
}