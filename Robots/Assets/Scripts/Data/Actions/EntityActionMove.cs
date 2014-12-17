using UnityEngine;
using System.Collections;

public class EntityActionMove : EntityTargetedAction
{
	public EntityActionMove(RunnableEntity owner, Vector3i target)
		: base(owner, target)
	{

	}

	protected override EntityActionResult _Run()
	{
		return owner.Move(_targetPosition) != -1 ? EntityActionResult.Success : EntityActionResult.Failure;
	}
}
