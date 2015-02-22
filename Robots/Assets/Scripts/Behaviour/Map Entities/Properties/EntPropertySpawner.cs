using UnityEngine;
using System.Collections.Generic;
using System.Xml;

public class EntPropertySpawner : EntProperty {

	[SerializeField]
	private MapPosition _position;
	[SerializeField]
	private uint _frequencySpawn=1;
	[SerializeField]
	private uint _numberSpawn=1;

	private Queue<RobotScript> _spawnQueue = new Queue<RobotScript>();

	private static uint _sinceLastSpawn = 0;
	private static uint _currentSpawnCount = 0;

	protected override void _Interact(EntityEvent action, MapEntity entity) {
		if (action == EntityEvent.Spawn || action == EntityEvent.Turn && _numberSpawn > 0) {
			if (_sinceLastSpawn >= _frequencySpawn) {
				RobotScript newRobot = _spawnQueue.Peek();
				if (GameData.currentState.AddEntity(newRobot, _position)) {
					_spawnQueue.Dequeue();
					_currentSpawnCount++;
					--_numberSpawn;
					_sinceLastSpawn = 0;
				}
			} else {
				++_sinceLastSpawn;
			}
		}
	}

	public override void SetParameters(Dictionary<string, string> parameters) {
		if (parameters.ContainsKey("position")) {
			_position = MapPosition.FromString(parameters["position"]);
		}
	}

	public void EnqueueRobotSpawn(RobotScript robot) {
		_spawnQueue.Enqueue(robot);
	}

	public uint GetNbSpawn() {
		return _numberSpawn;
	}

	public override XmlNode Serialize(XmlDocument doc) {
		var property = doc.CreateElement("property");

		var c = doc.CreateAttribute("class");
		c.Value = ToString();
		property.Attributes.Append(c);

		var param = doc.CreateAttribute("params");
		param.Value = "position="+_position.ToString();
		property.Attributes.Append(param);

		return property;
	}
}