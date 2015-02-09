using UnityEngine;
using System.Collections;

public class EntityActionPush : EntityInteraction
{
	public EntityActionPush(string ownerID, string targetID)
		: base(ownerID, targetID)
	{

	}

	protected override EntityActionResult _Run()
	{
		MapEntity target = GameData.currentState.entities[_targetID];
		target.Interact(EntityEvent.Push, owner);

		return EntityActionResult.Success; //target.Teleport(Map.GetLocalPos(2.0f * target.tr.position - owner.tr.position)) != -1 ? EntityActionResult.Success : EntityActionResult.Failure;
	}
}
