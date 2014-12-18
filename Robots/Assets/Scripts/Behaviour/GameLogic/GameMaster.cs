using UnityEngine;
using System.Collections;
using System.ComponentModel;

public class GameMaster : MonoBehaviour {
	private int _nbTurnMax;

	private uint _necessaryNbOfBot;
	private uint _nbOfBotExit;
	private uint _allNbOfBot;
	private uint _allNbOfDeath;

	void Start() {
		GameData.gameMaster = this;
		GameData.currentState = new GameState();
		// Init
		_nbOfBotExit = 0;
		_allNbOfBot = 0;
		_allNbOfDeath = 0;
		
		foreach (var property in MapEntity.entities.GetEnumerator().Current.Value.GetComponents<EntPropertySpawner>()) {
			_allNbOfBot += property.GetNbSpawn();
		}
	}

	public void CheckTurn(int turn) {
		if (turn >= _nbTurnMax && _nbTurnMax != -1) {
			// TODO : End Lose
		}
	}

	public void SetExitBot() {
		_nbOfBotExit++;
		if (_nbOfBotExit >= _nbTurnMax) {
			// TODO : End Win
		}
	}

	// TODO: use it
	public void SetDeathBot() {
		_allNbOfDeath++;
		if (_allNbOfBot - _allNbOfDeath <= _necessaryNbOfBot - _nbOfBotExit) {
			// TODO : End Lose
		}
	}
}
