using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class EntityStateChange : EntityAction
{ // Action of an entity on itself

	public EntityStateChange(string ownerID)
		: base(ownerID)
	{

	}

	public override Dictionary<string, string> XmlActionAttibutes()
	{
		Dictionary<string, string> attributes = new Dictionary<string, string>();

		return attributes;
	}
}
