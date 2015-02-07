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
	private static uint _currentSpawnCount = 0;

	protected override void _Interact(EntityEvent action, MapEntity entity) {
		if (action == EntityEvent.Spawn || action == EntityEvent.Turn && _numberSpawn > 0) {
			string robotId = "robot_" + this.owner.id + "_" + (_currentSpawnCount+1);
			RobotScript newRobot = (RobotScript)GameData.instantiateManager.entities[robotId];
			
			if (_sinceLastSpawn >= _frequencySpawn && GameData.currentState.map.SetEntity(newRobot, _position) == 0) {
				_currentSpawnCount++;
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
			_position = Map.GetWorldPos(Vector3i.FromString(parameters["position"]));
		}
	}

	public uint GetNbSpawn() {
		return _numberSpawn;
	}
}