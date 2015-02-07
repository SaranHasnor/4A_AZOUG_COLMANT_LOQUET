using UnityEngine;
using System.Collections.Generic;

public enum ActionRunMode
{
	Paused,			// Actions are paused
	Simulation,		// Playing the player's current actions
	Replay,			// Actions already validated and uneditable
	Action			// Synchronized playback between clients
}

public class TimeMaster : MonoBehaviour
{
	private List<RunnableEntity> _entities;
	private List<RunnableEntity> _pendingEntities;
	private float _lastRunTime;
	private ActionRunMode _runMode;

	public void ToggleRun()
	{
		_runMode = _runMode == ActionRunMode.Simulation ? ActionRunMode.Paused : ActionRunMode.Simulation;
	}

	private int _currentTurn;
	public int currentTurn
	{
		get
		{
			return _currentTurn;
		}
	}

	private void NextTurn()
	{
		_currentTurn++;
		GameData.gameMaster.CheckTurn(_currentTurn);
	}

	void Start()
	{
		GameData.timeMaster = this;

		_entities = new List<RunnableEntity>();
		_pendingEntities = new List<RunnableEntity>();
		_runMode = ActionRunMode.Paused;

		_currentTurn = 0;
	}

	public void RegisterEntity(RunnableEntity entity)
	{
		if (_entities.Contains(entity))
		{
			Debug.LogWarning("Entity " + entity.name + " tried to register twice");
			return;
		}

		_entities.Add(entity);
	}

	private void RunActions()
	{
		foreach (MapEntity entity in GameData.currentState.entities.Values)
		{
			if (entity is RunnableEntity)
			{
				EntityActionResult res = ((RunnableEntity)entity).RunNextAction();

				if (res == EntityActionResult.Pending)
				{
					_pendingEntities.Add((RunnableEntity)entity);
				}
			}
			else if (entity is BlockScript)
			{
				((BlockScript)entity).Interact(EntityEvent.Turn, null);
			}
		}

		NextTurn();
		_lastRunTime = Time.time;
	}

	void Update()
	{ // While simulation or execution is running, run one action every second
		if (_runMode == ActionRunMode.Simulation)
		{
			if (Time.time - _lastRunTime >= 1.0f)
			{
				if (_pendingEntities.Count == 0)
				{
					RunActions();
				}
				else if (Time.time - _lastRunTime >= 3.0f)
				{ // Failsafe
					List<RunnableEntity> pendingEntities = new List<RunnableEntity>(_pendingEntities);
					foreach (RunnableEntity entity in pendingEntities)
					{
						Debug.LogError(entity.name + " is stuck");

						entity.StopCurrentAction();
						EntityCompletedAction(entity, entity.ActionAtTime(), EntityActionResult.Error);
						entity.MoveToTime(1, true);
					}
				}
			}
		}
	}

	public void EntityCompletedAction(RunnableEntity entity, EntityAction action, EntityActionResult result)
	{ // Callback for entities performing actions over a duration
		if (!_pendingEntities.Contains(entity))
		{
			Debug.LogWarning("Got unexpected action callback from entity " + entity.name);
			return;
		}

		_pendingEntities.Remove(entity);
	}
}
