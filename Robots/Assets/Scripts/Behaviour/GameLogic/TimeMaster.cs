using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeMaster : MonoBehaviour
{
	private List<RunnableEntity> entities;

	//private List<RunnableEntity> pendingEntities;

	void Start()
	{
		GameData.timeMaster = this;

		this.entities = new List<RunnableEntity>();

		//this.pendingEntities = new List<RunnableEntity>();
	}

	public void RegisterEntity(RunnableEntity entity)
	{
		if (this.entities.Contains(entity))
		{
			Debug.LogWarning("Entity " + entity.name + " tried to register twice");
			return;
		}

		this.entities.Add(entity);
	}

	private void RunActions()
	{
		/*foreach (RunnableEntity entity in this.entities)
		{
			
		}*/

		throw new System.NotImplementedException();
	}

	void Update()
	{ // While simulation or execution is running, run one action every second

	}

	public void EntityCompletedAction(RunnableEntity entity, EntityAction action, EntityActionResult result)
	{ // Callback for entities performing actions over a duration
		throw new System.NotImplementedException();
	}
}
