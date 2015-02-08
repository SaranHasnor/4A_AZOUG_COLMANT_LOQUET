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

	private static MapPosition _uninitializedPos = new MapPosition(0, 0, 1500);

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

	private GameObject _BlockPrefabForType(string type)
	{
		if (!_blockLibrary.blocks.ContainsKey(type))
		{
			Debug.LogError("Tried to create unknown block type " + type);
			return null;
		}
		return _blockLibrary.blocks[type];
	}

	private MapEntity _SpawnEntity(GameObject prefab, string id, MapPosition position, Team team = Team.None)
	{ // TODO: Create a position where all uninitialized entities go
		GameObject result = GameObject.Instantiate(prefab, Vector3.zero, Quaternion.identity) as GameObject;
		MapEntity script = result.GetComponent<MapEntity>();
		script.InitializeMapEntity(id, position, team);

		_entities.Add(id, script);

		return script;
	}

	private MapEntity _SpawnEntity(GameObject prefab)
	{
		string id = "entity_" + _entities.Count.ToString();

		return _SpawnEntity(prefab, id, _uninitializedPos);
	}

	public RobotScript SpawnRobot(string id, MapPosition position, Team team)
	{
		return _SpawnEntity(_robotPrefab, id, position, team) as RobotScript;
	}

	public RobotScript SpawnRobot(string id, Team team)
	{
		return _SpawnEntity(_robotPrefab, id, _uninitializedPos, team) as RobotScript;
	}

	public BlockScript SpawnBlock(string type, string id, MapPosition position, Team team)
	{
		return _SpawnEntity(_BlockPrefabForType(type), id, position, team) as BlockScript;
	}

	public BlockScript SpawnBlock(string type)
	{
		return _SpawnEntity(_BlockPrefabForType(type)) as BlockScript;
	}
}
