using UnityEngine;
using System.Collections.Generic;
using System.Xml;

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
		this.cursor = Mathf.Clamp(relative ? cursor + time : time, 0, queue.Count);
	}

	public int GetCursor()
	{
		return this.cursor;
	}

	public EntityAction GetAction(int timeOverride = -1)
	{
		int time = (timeOverride < 0) ? this.cursor : timeOverride;

		return queue[time];
	}

	public XmlDocument ToXml()
	{ // Serialize all the actions we're holding and put them together
		XmlDocument doc = new XmlDocument();
		XmlElement node = doc.CreateElement("queue");

		Dictionary<string, string> attributes = new Dictionary<string, string>();

		attributes.Add("id", queue[0].owner.id); // Temporary

		foreach (KeyValuePair<string, string> attribute in attributes)
		{
			node.SetAttribute(attribute.Key, attribute.Value);
		}

		foreach (EntityAction action in this.queue)
		{
			node.AppendChild(action.ToXml());
		}

		return doc;
	}

	public static ActionQueue CreateFromXmlNode(XmlNode node)
	{
		ActionQueue result = new ActionQueue();
		foreach (XmlNode actionNode in node)
		{
			if (actionNode.Name == "action")
			{
				result.SetAction(EntityAction.CreateFromXmlNode(actionNode, node.Attributes["id"].Value));
			}
		}
		result.SetCursor(0);
		return result;
	}

	public void Insert(EntityAction action) {
		queue.Insert(cursor, action);
	}
}
