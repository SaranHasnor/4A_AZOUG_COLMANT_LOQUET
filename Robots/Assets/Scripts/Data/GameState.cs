using UnityEngine;
using System.Collections;

public class GameState
{ // Represents a snapshot of the game at a given time
	private Map _map;

	public Map map
	{
		get
		{
			return _map;
		}
	}

	public GameState(Map map)
	{
		_map = map;
	}

	public static GameState Parse(string text)
	{
		throw new System.NotImplementedException();
	}

	public void Save()
	{
		throw new System.NotImplementedException();
	}
}
