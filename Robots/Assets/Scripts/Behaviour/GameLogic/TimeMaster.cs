using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimeMaster : MonoBehaviour
{
	private List<RunnableEntity> entities;

	void Start()
	{
		GameData.timeMaster = this;

		this.entities = new List<RunnableEntity>();
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

	void Update()
	{ // While simulation or execution is running, run one action every second

	}
}
