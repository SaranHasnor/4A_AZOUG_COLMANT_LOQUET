using UnityEngine;
using System.Collections;
using System.ComponentModel;

public class GameMaster : MonoBehaviour {
	private int _nbTurnMax;

	private uint _necessaryNbOfBot;
	private uint _nbOfBot;

	void Start() {
		GameData.currentState = new GameState();
		// Init
		_nbOfBot = 0;
	}

	public void CheckTurn(int turn) {
		if (turn >= _nbTurnMax && _nbTurnMax != -1) {
			// TODO : End Lose
		}
	}

	public void SetExitBot() {
		_nbOfBot++;
		if (_nbOfBot >= _nbTurnMax) {
			// TODO : End Win
		}
	}
}
