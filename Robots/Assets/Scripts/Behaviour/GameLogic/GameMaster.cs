using UnityEngine;
using System.Collections;

public class GameMaster : MonoBehaviour
{
	void Start()
	{
		GameData.currentState = new GameState();
	}
}
