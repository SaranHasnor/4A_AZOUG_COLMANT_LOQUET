using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class RunnableEntity : MapEntity
{ // Describes an entity that should be run by the TimeMaster every turn
	protected ActionQueue _queue;

	public List<EntityAction> actions
	{
		get
		{
			return _queue.actions;
		}
	}

	protected void InitializeRunnableEntity(/*Team team = Team.None, string id = null, */ActionQueue actionQueue = null)
	{
		/*base.InitializeMapEntity(team, id);*/

		this._queue = actionQueue ?? new ActionQueue();

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

	public void SetAction(EntityAction action, int time = -1)
	{
		_queue.SetAction(action, time);
	}
}
