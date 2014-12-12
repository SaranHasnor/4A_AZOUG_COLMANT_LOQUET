using UnityEngine;
using System.Collections;

public enum EntityActionResult
{
	Success,		// Action executed succesfully
	Failure,		// Action failed to execute
	Repeat,			// Action should be repeated
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

	public static EntityAction LoadFromString(string s)
	{
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
