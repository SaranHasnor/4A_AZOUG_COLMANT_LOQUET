using UnityEngine;
using System.Collections.Generic;

public class EntPropertyTeleporter : EntProperty {
	[SerializeField]
	private string _targetID;

	[SerializeField]
	private uint _frequencyTeleport = 1;

	private static uint _sinceLastTeleport = 0;

	protected override void _Interact(EntityEvent action, MapEntity entity)
	{
		if (action == EntityEvent.Collide)
		{
			Debug.Log("Collision");
			if (_sinceLastTeleport >= _frequencyTeleport)
			{
				if (entity.Move(GameData.currentState.entities[_targetID].localPosition))
				{
					_sinceLastTeleport = 0;
				}
			}
			else
			{
				++_sinceLastTeleport;
			}
		}
	}

	public override void SetParameters(Dictionary<string, string> parameters)
	{
		_targetID = parameters["target"];
	}
}