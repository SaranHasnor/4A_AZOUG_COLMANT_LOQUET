using UnityEngine;

public class GameMaster : MonoBehaviour
{
	[SerializeField]
	private int _maxTurns = -1;

	private uint _robotGoalCount;
	private uint _robotExitCount;
	private uint _robotTotalCount;
	private uint _robotDeadCount;

	void Start()
	{
		GameData.gameMaster = this;
	}

	public void DidLoadMap()
	{ // Notifies the game master that a new map has been loaded
		foreach (MapEntity entity in GameData.currentState.entities.Values)
		{
			foreach (EntProperty property in entity.properties)
			{
				if (property is EntPropertySpawner)
				{
					EntPropertySpawner spawner = property as EntPropertySpawner;

					uint count = spawner.GetNbSpawn();
					string prefix = "robot_" + spawner.owner.id + "_";

					for (uint i = 0; i < count; i++)
					{
						string id = prefix + count;
						GameData.instantiateManager.SpawnRobot(id, spawner.owner.team);
						_robotTotalCount++;
					}

					_robotGoalCount += count;
				}
			}
		}
	}

	public void CheckTurn(int turn)
	{
		if (turn >= _maxTurns && _maxTurns != -1)
		{
			// TODO : End Lose
		}
	}

	public void OnBotExit()
	{
		_robotExitCount++;
		if (_robotExitCount >= _maxTurns)
		{
			// TODO : End Win
		}
	}

	// TODO: use it
	public void OnBotDeath()
	{
		_robotDeadCount++;
		if (_robotTotalCount - _robotDeadCount <= _robotGoalCount - _robotExitCount)
		{
			// TODO : End Lose
		}
	}
}
