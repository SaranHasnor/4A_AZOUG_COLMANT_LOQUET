using UnityEngine;
using System.Collections;

public class EntityActionMove : EntityTargetedAction
{
	public EntityActionMove(string ownerID, MapPosition target)
		: base(ownerID, target)
	{

	}

	protected override EntityActionResult _Run()
	{
		return owner.Teleport(_targetPosition) != -1 ? EntityActionResult.Success : EntityActionResult.Failure;
	}
}
