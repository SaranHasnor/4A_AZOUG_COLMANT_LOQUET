using UnityEngine;
using System.Collections;

public class RunnableEntity : MonoBehaviour
{
	protected ActionQueue queue;

	protected void Initialize()
	{
		this.queue = new ActionQueue();

		// Notify the Time Master of our existence so he can manage our action queue
		GameData.timeMaster.RegisterEntity(this);
	}

	// Describes an entity that should be run by the TimeMaster every turn
	// "Abstract" class, should not be implemented as-is
}
