using UnityEngine;
using System.Collections;

public abstract class RunnableEntity : MapEntity
{ // Describes an entity that should be run by the TimeMaster every turn
	protected ActionQueue _queue;

	protected override void Initialize()
	{
		base.Initialize();

		this._queue = new ActionQueue();

		// Notify the Time Master of our existence so he can manage our action queue
		GameData.timeMaster.RegisterEntity(this);
	}

	public EntityActionResult RunNextAction()
	{
		return _queue.Run();
	}

	public void StopCurrentAction()
	{

	}

	public void MoveToTime(int time, bool relative = false)
	{
		_queue.SetCursor(time, relative);

		// TODO: Find out where we would be at this time
	}

	public EntityAction ActionAtTime(int time = -1)
	{
		return _queue.GetAction(time);
	}
}
