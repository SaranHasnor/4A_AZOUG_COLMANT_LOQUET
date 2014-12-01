using UnityEngine;
using System.Collections;

public class EntityStateChange : EntityAction
{
	// Action involving only one entity

	public EntityStateChange(RunnableEntity owner)
		: base(owner)
	{

	}

	public override void Run()
	{

	}
}
