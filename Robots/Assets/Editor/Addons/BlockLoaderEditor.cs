using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;

[CustomEditor(typeof(BlockLibrary))]
public class BlockLoaderEditor : Editor
{
	public override void OnInspectorGUI()
	{
		BlockLibrary library = (BlockLibrary)target;

		List<KeyValuePair<string, GameObject>> temp = new List<KeyValuePair<string,GameObject>>();

		EditorGUILayout.LabelField("Block list:");

		bool valid = true;
		int count = 0;
		while (valid)
		{
			valid = false;
			bool existed = (library.blocks != null && library.blocks.Count > count);

			EditorGUILayout.BeginHorizontal();

			string name = EditorGUILayout.TextField(existed ? library.blockList[count].Key : "");
			GameObject prefab = (GameObject)EditorGUILayout.ObjectField(existed ? library.blockList[count].Value : null, typeof(GameObject), false);
			
			EditorGUILayout.EndHorizontal();

			if (name.Length > 0)
			{
				temp.Add(new KeyValuePair<string, GameObject>(name, prefab));
				count++;

				if (prefab != null && prefab.GetComponent<BlockScript>() != null)
				{
					valid = true;
				}
			}
		}

		library.blockList = temp;
	}
}
