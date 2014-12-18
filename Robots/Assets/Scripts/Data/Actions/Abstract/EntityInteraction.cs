using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class EntityInteraction : EntityAction
{ // Action of an entity on another entity
	private RunnableEntity _target;

	public EntityInteraction(RunnableEntity owner, RunnableEntity target)
		: base(owner)
	{
		_target = target;
	}

	public override Dictionary<string, string> XmlActionAttibutes()
	{
		Dictionary<string, string> attributes = new Dictionary<string, string>();

		attributes.Add("target", _target.id);

		return attributes;
	}
}
