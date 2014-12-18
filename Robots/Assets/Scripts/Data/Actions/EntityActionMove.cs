using UnityEngine;
using System.Collections;

public class EntityActionMove : EntityTargetedAction
{
	public EntityActionMove(string ownerID, Vector3i target)
		: base(ownerID, target)
	{

	}

	protected override EntityActionResult _Run()
	{
		return owner.Move(_targetPosition) != -1 ? EntityActionResult.Success : EntityActionResult.Failure;
	}
}
