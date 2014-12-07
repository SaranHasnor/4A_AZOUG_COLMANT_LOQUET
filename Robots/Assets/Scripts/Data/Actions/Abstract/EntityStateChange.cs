using UnityEngine;
using System.Collections;

public abstract class EntityStateChange : EntityAction
{ // Action of an entity on itself

	public EntityStateChange(RunnableEntity owner)
		: base(owner)
	{

	}

	public override string ToString()
	{
		return "<" + this.GetType().ToString() + ">";
	}
}
