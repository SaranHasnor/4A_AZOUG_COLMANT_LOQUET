using UnityEngine;
using System.Collections;

public abstract class EntityTargetedAction : EntityAction
{ // Action of an entity on an empty cell
	protected Vector3i _targetPosition;

	public EntityTargetedAction(RunnableEntity owner, Vector3i targetPosition)
		: base(owner)
	{
		_targetPosition = targetPosition;
	}

	public override string ToString()
	{
		return "<" + this.GetType().ToString() + ":" + _targetPosition.ToString() + ">";
	}
}
