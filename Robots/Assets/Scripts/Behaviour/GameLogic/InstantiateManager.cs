using UnityEngine;
using System.Collections;

public class InstantiateManager : MonoBehaviour
{
	[SerializeField]
	private BlockLibrary _blockLibrary;

	[SerializeField]
	private GameObject _robotPrefab;

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
