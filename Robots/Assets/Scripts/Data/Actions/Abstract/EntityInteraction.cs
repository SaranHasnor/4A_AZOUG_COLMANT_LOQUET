using UnityEngine;
using System.Collections;

public abstract class EntityInteraction : EntityAction
{ // Action of an entity on another entity
	private RunnableEntity _target;

	public EntityInteraction(RunnableEntity owner, RunnableEntity target)
		: base(owner)
	{
		_target = target;
	}

	public override string ToString()
	{ // FIXME: Create our own IDs
		return "<" + this.GetType().ToString() + ":" + _target.networkView.viewID + ">";
	}
}
