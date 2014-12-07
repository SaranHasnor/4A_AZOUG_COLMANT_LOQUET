using UnityEngine;
using System.Collections;

public abstract class EntityTargetedAction : EntityAction
{ // Action of an entity on an empty cell
	private Vector3 _targetPosition;

	public EntityTargetedAction(RunnableEntity owner, Vector3 targetPosition)
		: base(owner)
	{
		_targetPosition = targetPosition;
	}

	public override string ToString()
	{
		return "<" + this.GetType().ToString() + ":" + _targetPosition.ToString() + ">";
	}
}
