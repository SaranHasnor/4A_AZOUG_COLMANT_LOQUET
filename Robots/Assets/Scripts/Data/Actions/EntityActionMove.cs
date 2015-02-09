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
		MapDirection movement = MapDirection.DirectionToMove(owner.localPosition, _targetPosition);
		
		return owner.Move(owner.localPosition + movement) ? EntityActionResult.Success : EntityActionResult.Failure;
	}
}
