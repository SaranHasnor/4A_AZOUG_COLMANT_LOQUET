using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockLibrary : MonoBehaviour
{
	[SerializeField]
	public List<KeyValuePair<string, GameObject>> blockList;

	private Dictionary<string, GameObject> _blocks;
	public Dictionary<string, GameObject> blocks
	{
		get
		{
			return _blocks;
		}
	}

	public GameObject stonePrefab;
	public GameObject sandPrefab;
	public GameObject spawnerPrefab;
	public GameObject teleporterPrefab;
	public GameObject targetPrefab;

	void Start()
	{ // Load the blocks
		_blocks = new Dictionary<string, GameObject>();
		/*if (this.blockList != null)
		{
			foreach (KeyValuePair<string, GameObject> pair in this.blockList)
			{
				_blocks.Add(pair.Key, pair.Value);
			}
		}*/

		// TEMPORARY:
		_blocks.Add("stoneblock", stonePrefab);
		_blocks.Add("sandblock", sandPrefab);
		_blocks.Add("spawner", spawnerPrefab);
		_blocks.Add("teleporter", teleporterPrefab);
		_blocks.Add("teleporter_target", targetPrefab);
	}
}
