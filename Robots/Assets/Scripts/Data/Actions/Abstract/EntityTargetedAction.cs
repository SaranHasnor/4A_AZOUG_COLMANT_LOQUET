using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class EntityTargetedAction : EntityAction
{ // Action of an entity on an empty cell
	protected MapPosition _targetPosition;

	public EntityTargetedAction(string ownerID, MapPosition targetPosition)
		: base(ownerID)
	{
		_targetPosition = targetPosition;
	}

	public override Dictionary<string,string> XmlActionAttibutes()
	{
		Dictionary<string, string> attributes = new Dictionary<string, string>();

		attributes.Add("position", _targetPosition.ToString());

		return attributes;
	}
}
