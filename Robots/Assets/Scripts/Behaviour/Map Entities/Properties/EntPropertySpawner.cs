using UnityEngine;
using System.Collections.Generic;

public class EntPropertySpawner : EntProperty {

	[SerializeField]
	private Vector3 _position;
	[SerializeField]
	private uint _frequencySpawn=1;
	[SerializeField]
	private uint _numberSpawn=1;

	private static uint _sinceLastSpawn = 0;

	protected override void _Interact(EntityEvent action, MapEntity entity) {
		if (action == EntityEvent.Spawn || action == EntityEvent.Turn && _numberSpawn > 0) {
			GameObject go = (GameObject)GameObject.Instantiate(GameData.instantiateManager.robotPrefab, _position, Quaternion.identity);
			RobotScript script = go.GetComponent<RobotScript>();
			// Initialize it please ;_;

			if(_sinceLastSpawn >= _frequencySpawn && GameData.currentState.map.AddEntity(script, GameData.currentState.map.ToLocalPos(_position)) == 0)
			{
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
			_position = GameData.currentState.map.ToWorldPos(MapPosition.FromString(parameters["position"]));
		}
	}

	public uint GetNbSpawn() {
		return _numberSpawn;
	}
}