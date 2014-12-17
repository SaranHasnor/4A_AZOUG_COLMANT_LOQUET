using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionQueue
{
	private List<EntityAction> queue;
	private int cursor;

	public int length
	{
		get
		{
			return queue.Count;
		}
	}

	public List<EntityAction> actions
	{
		get
		{
			return new List<EntityAction>(queue);
		}
	}

	public ActionQueue()
	{
		this.queue = new List<EntityAction>();
		this.cursor = 0;
	}

	public ActionQueue(string data)
	{
		this.queue = new List<EntityAction>();
		this.cursor = 0;
	}

	public void SetAction(EntityAction action, int timeOverride = -1)
	{
		int time = (timeOverride < 0) ? this.cursor : timeOverride;

		while (queue.Count <= time)
		{ // Set the new time frame by filling empty spaces with null actions
			queue.Add(null);
		}

		queue[time] = action;

		if (timeOverride < 0) this.cursor++;
	}

	public void RemoveAction(int timeOverride = -1)
	{
		int time = (timeOverride < 0) ? this.cursor : timeOverride;

		if (time < queue.Count)
		{
			queue[time] = null;

			if (timeOverride < 0) this.cursor--;
		}
	}

	public void MoveAction(int source, int target)
	{ // Useful?

	}

	public EntityActionResult Run(int timeOverride = -1)
	{
		int time = (timeOverride < 0) ? this.cursor : timeOverride;

		if (time < queue.Count)
		{
			EntityActionResult res = queue[time].Run();

			if (res != EntityActionResult.Repeat)
			{
				if (timeOverride < 0) this.cursor++;
			}

			return res;
		}

		return EntityActionResult.Success; // Ran out of actions, don't bother the boss with useless messages
	}

	public void SetCursor(int time, bool relative = false)
	{
		this.cursor = Mathf.Clamp(relative?cursor+time:time, 0, queue.Count);
	}

	public EntityAction GetAction(int timeOverride = -1)
	{
		int time = (timeOverride < 0) ? this.cursor : timeOverride;

		return queue[time];
	}

	public string Save()
	{ // Serialize all the actions we're holding and put them together
		return null;
	}
}
