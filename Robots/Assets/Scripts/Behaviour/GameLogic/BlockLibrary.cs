using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockLibrary : MonoBehaviour
{
	public List<KeyValuePair<string, GameObject>> blockList;

	private Dictionary<string, GameObject> _blocks;
	public Dictionary<string, GameObject> blocks
	{
		get
		{
			return _blocks;
		}
	}

	void Start()
	{ // Load the blocks
		GameData.blockLibrary = this;

		_blocks = new Dictionary<string, GameObject>();
		if (this.blockList != null)
		{
			foreach (KeyValuePair<string, GameObject> pair in this.blockList)
			{
				_blocks.Add(pair.Key, pair.Value);
			}
		}
	}
}
