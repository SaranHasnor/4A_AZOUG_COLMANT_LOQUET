public class EntPropertyRunnable : EntProperty
{
	public ActionQueue actionQueue
	{ // For convinience I guess (I don't really like making this public because it can be modified, but we don't filter access to GameData so it doesn't matter anyway)
		get
		{
			return GameData.currentState.actions[this.owner.id];
		}
	}

	public EntityActionResult RunNextAction()
	{
		return this.actionQueue.Run();
	}

	public void StopCurrentAction()
	{ // In case it's pending, etc.

	}

	public void MoveToTime(int time, bool relative = false)
	{
		this.actionQueue.SetCursor(time, relative);

		// TODO: Find out where we would be at this time
	}

	public EntityAction ActionAtTime(int time = -1)
	{
		return this.actionQueue.GetAction(time);
	}

	public void SetAction(EntityAction action, int time = -1)
	{
		this.actionQueue.SetAction(action, time);
	}

	protected override void _Interact(EntityEvent actionType, MapEntity entity)
	{
		if (actionType == EntityEvent.Turn)
		{
			this.RunNextAction();
		}
	}
}
