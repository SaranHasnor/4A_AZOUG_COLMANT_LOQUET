using UnityEngine;
using System.Collections;

public class EntityActionMove : EntityTargetedAction
{
	public EntityActionMove(RunnableEntity owner, Vector3 target)
		: base(owner, target)
	{

	}

	protected override EntityActionResult _Run()
	{
		// TODO: Call the map and make it move us if possible
		return owner.Move(_targetPosition) != -1 ? EntityActionResult.Success : EntityActionResult.Failure;
	}
}
