using UnityEngine;
using System.Collections.Generic;

public class EntPropertySpawner : EntProperty {
	[SerializeField]
	private GameObject _prefBot;
	[SerializeField]
	private Vector3 _position;
	[SerializeField]
	private uint _frequencySpawn=1;
	[SerializeField]
	private uint _numberSpawn=1;

	private static uint _sinceLastSpawn = 0;

	protected override void _Interact(EntityEvent action, MapEntity entity) {
		if (action == EntityEvent.Spawn || action == EntityEvent.Turn && _numberSpawn > 0) {
			if (_sinceLastSpawn >= _frequencySpawn && GameData.currentState.map.SetEntity(_prefBot, _position) == 0) {
				--_numberSpawn;
				_sinceLastSpawn = 0;
			} else {
				++_sinceLastSpawn;
			}
		}
	}

	public override void SetParameters(Dictionary<string, string> parameters)
	{
		if (parameters.ContainsKey("position"))
		{
			//_position = Vector3i.FromString(parameters["position"]);
		}
	}

	public uint GetNbSpawn() {
		return _numberSpawn;
	}
}