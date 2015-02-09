using UnityEngine;

public class EntPropertyTeleporter : EntProperty {
	[SerializeField]
	private EntPropertyTeleporterTarget _target;

	[SerializeField]
	private uint _frequencyTeleport = 1;

	private static uint _sinceLastTeleport = 0;

	protected override void _Interact(EntityEvent action, MapEntity entity)
	{
		if (action == EntityEvent.Teleport || action == EntityEvent.Collide)
		{
			if(_sinceLastTeleport >= _frequencyTeleport && GameData.currentState.map.GetEntity(entity).Move(GameData.currentState.map.ToLocalPos(_target.gameObject.transform.position)))
				_sinceLastTeleport = 0;
			else
				++_sinceLastTeleport;
		}
	}

	/// <summary><c>SetParameters</c> is a function to parameterize a property.</summary>
	/// <param name="Frequence">It's the frequence of teleportation by turn.</param>
	public void SetParameters(uint Frequence) { // TODO Amau : Fix SetParameters.
		_frequencyTeleport = Frequence;
	}
}