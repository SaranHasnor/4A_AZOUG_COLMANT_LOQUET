﻿using UnityEngine;
using System.Collections.Generic;

public class EntPropertyTeleporterTarget : EntProperty
{
	[SerializeField]
	private MapPosition _exitDirection = MapDirection.up;

	protected override void _Interact(EntityEvent actionType, MapEntity entity)
	{
		if (actionType == EntityEvent.Teleport)
		{
			entity.Move(owner.localPosition + _exitDirection);
		}
	}

	public override void SetParameters(Dictionary<string, string> parameters)
	{
		if (parameters.ContainsKey("exitDirection"))
		{
			_exitDirection = MapDirection.FromString(parameters["exitDirection"]);
		}
	}
}