using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InstantiateManager : MonoBehaviour
{
	[SerializeField]
	private BlockLibrary _blockLibrary;

	[SerializeField]
	private GameObject _robotPrefab;

	// This is used to keep track of the entities even outside of the current game state
	private Dictionary<string, MapEntity> _entities;
	public Dictionary<string, MapEntity> entities
	{
		get
		{
			return new Dictionary<string, MapEntity>(_entities);
		}
	}

	void Start()
	{
		GameData.instantiateManager = this;

		_entities = new Dictionary<string, MapEntity>();
	}

	public GameObject robotPrefab
	{
		get
		{
			return _robotPrefab;
		}
	}

	public GameObject BlockPrefabForType(string type)
	{
		if (!_blockLibrary.blocks.ContainsKey(type))
		{
			Debug.LogError("Tried to create unknown block type " + type);
			return null;
		}
		return _blockLibrary.blocks[type];
	}

	public void SpawnRobot(Team team, string id)
	{ // TODO: Put them really far away
		GameObject result = GameObject.Instantiate(_robotPrefab, Vector3.zero, Quaternion.identity) as GameObject;
		RobotScript script = result.GetComponent<RobotScript>();

		_entities.Add(id, script);

		script.team = team;
	}

	/*public BlockScript CreateBlock(string type, Vector3i pos, int team = -1)
	{
		if (!_blockLibrary.blocks.ContainsKey(type))
		{
			Debug.LogError("Tried to create unknown block type " + type);
			return null;
		}

		GameObject prefab = _blockLibrary.blocks[type];
		//GameObject result = GameObject.Instantiate(prefab, Map.GetLocalPos(pos), Quaternion.identity);
		BlockScript script = result.getComponent<BlockScript>();

		return script;
	}

	public RobotScript SpawnRobot(Vector3i pos, int team = -1)
	{
		//GameObject result = GameObject.Instantiate(_robotPrefab, Map.GetLocalPos(pos), Quaternion.identity);
		RobotScript script = result.getComponent<RobotScript>();

		return script;
	}*/
}
