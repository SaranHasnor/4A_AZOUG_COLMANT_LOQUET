using UnityEngine;

public class BlockScript : MapEntity
{
	// The only argument for making this a runnable entity is to update their state each turn
	// Maybe we could also have blocks take actions at specific turns, but I'd like this to be a property instead

	void Start()
	{
		base.Initialize();
	}
}
