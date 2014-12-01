using UnityEngine;
using System.Collections;

// Serializable data
public abstract class EntityAction
{
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

	public static EntityAction loadFromString(string s)
	{
		return null;
	}

	public abstract void Run();

	// Actually, see if we can find a class that implements serialization methods
	//public abstract void Serialize();
}
