using UnityEngine;
using System.Collections;
using System.Xml;

public enum EntityActionResult
{
	Success,		// Action executed succesfully
	Failure,		// Action failed to execute
	Repeat,			// Action should be repeated
	Pending,		// Action works over a duration and will return later (seems unnatural, keeping it just in case)
	Error			// An error occured while trying to execute the action
}

public abstract class EntityAction
{ // Generic action, subclasses should only be action categories, which should in turn be subclassed for actual actions
	protected RunnableEntity _owner;

	public RunnableEntity owner
	{
		get
		{
			return _owner;
		}
	}

	protected EntityAction(RunnableEntity owner)
	{
		this._owner = owner;
	}

	public static EntityAction createFromXMLNode(XmlNode node)
	{
		/*owner.gameObject = */
		//Instantiate(GameData.blockLibrary.blocks[node.ChildNodes[0].Attributes["type"].Value]);
		//owner.id = node.ChildNodes[0].Attributes != null ? node.ChildNodes[0].Attributes["id"].Value : null;

		if(node.ChildNodes[0].Attributes["class"] != null)
		{
			var actionClass = node.ChildNodes[0].Attributes["class"].Value;
		}
		if(node.ChildNodes[0].Attributes["position"] != null)
		{
			var actionPosition = node.ChildNodes[0].Attributes["position"].Value;
		}
		if(node.ChildNodes[0].Attributes["target"] != null)
		{
			var actionTarget = node.ChildNodes[0].Attributes["target"].Value;
		}
		return null;
	}

	protected abstract EntityActionResult _Run();

	public EntityActionResult Run()
	{
		try
		{
			return this._Run();
		}
		catch (ActionException e)
		{
			Debug.LogException(e);
			return EntityActionResult.Error;
		}
	}

	public override abstract string ToString(); // I never thought I'd have to do this
}
