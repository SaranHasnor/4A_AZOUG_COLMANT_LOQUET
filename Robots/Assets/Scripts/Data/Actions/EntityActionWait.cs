using UnityEngine;
using System.Collections;

public class EntityActionWait : EntityStateChange
{
	public EntityActionWait(string ownerID)
		: base(ownerID)
	{

	}

	protected override EntityActionResult _Run()
	{
		return EntityActionResult.Success;
	}
}
