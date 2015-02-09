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
		return owner.Move(_targetPosition) ? EntityActionResult.Success : EntityActionResult.Failure;
	}
}
