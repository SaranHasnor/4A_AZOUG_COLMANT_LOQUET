using UnityEngine;
using System.Collections;

public abstract class RunnableEntity : MapEntity
{ // Describes an entity that should be run by the TimeMaster every turn
	protected ActionQueue _queue;

	protected void Initialize()
	{
		this._queue = new ActionQueue();

		// Notify the Time Master of our existence so he can manage our action queue
		GameData.timeMaster.RegisterEntity(this);
	}
}
