﻿using UnityEngine;

public enum ActionRunMode
{
	Paused,			// Actions are paused
	Simulation,		// Playing the player's current actions
	Replay,			// Actions already validated and uneditable
	Action			// Synchronized playback between clients
}

public class TimeMaster : MonoBehaviour
{
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

	void Start()
	{
		GameData.timeMaster = this;

		_runMode = ActionRunMode.Paused;

		_currentTurn = 0;
	}

	private void NextTurn()
	{
		_currentTurn++;
		GameData.gameMaster.CheckTurn(_currentTurn);
	}

	private void RunActions()
	{
		foreach (MapEntity entity in GameData.currentState.entities.Values)
		{
			entity.Interact(EntityEvent.Turn, null);
		}

		NextTurn();
		_lastRunTime = Time.time;
	}

	void Update()
	{ // While simulation or execution is running, run one action every second
		if (_runMode == ActionRunMode.Simulation)
		{
			if (GameData.gameMaster.isVictory() || GameData.gameMaster.isLose())
				return;

			if (Time.time - _lastRunTime >= 1.0f)
			{
				RunActions();
			}
		}
	}

	public int GetTurn()
	{
		return _currentTurn;
	}
}
