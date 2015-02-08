using UnityEngine;

public class EntPropertyTeleporter : EntProperty
{
	[SerializeField]
	private EntPropertyTeleporterTarget _target;

	[SerializeField]
	private uint _frequencyTeleport = 1;

	private static uint _sinceLastTeleport = 0;

	protected override void _Interact(EntityEvent action, MapEntity entity)
	{
		if (action == EntityEvent.Teleport || action == EntityEvent.Collide)
		{
			if(_sinceLastTeleport >= _frequencyTeleport && GameData.currentState.map.GetEntity(entity).Teleport(GameData.currentState.map.ToLocalPos(_target.gameObject.transform.position)) == 0)
				_sinceLastTeleport = 0;
			else
				++_sinceLastTeleport;
		}
	}
}