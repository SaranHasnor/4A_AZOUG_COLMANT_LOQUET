using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class EntityInteraction : EntityAction
{ // Action of an entity on another entity
	private string _targetID;

	public EntityInteraction(string ownerID, string targetID)
		: base(ownerID)
	{
		_targetID = targetID;
	}

	public override Dictionary<string, string> XmlActionAttibutes()
	{
		Dictionary<string, string> attributes = new Dictionary<string, string>();

		attributes.Add("target", _targetID);

		return attributes;
	}
}
