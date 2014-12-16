using System;
using UnityEngine;
using System.IO;

public class MapLoader : MonoBehaviour {

    [SerializeField]
    public GameObject blockPrefab;
    // Builds the attached map
    void Start() {
        // Read the file as one string.
        //maybe add a field "type of block" in the file
        const string filePath = @"..\..\..\map.csv";
        if (!File.Exists(filePath))
            Debug.LogError("Error : filePath not correct");
        else {
            try {
                using (var sr = new StreamReader(filePath)) {
                    string line;
                    while ((line = sr.ReadLine()) != null) {
                        var s = line.Split(';');
                        var go = GameData.blockLibrary.blocks[s[0]];
                        var pos = new Vector3(  float.Parse(s[1][1].ToString()),
                                                float.Parse(s[1][3].ToString()),
                                                float.Parse(s[1][5].ToString()) );
                        go.transform.position = pos;
                        go.SetActive(Boolean.Parse(s[2])); //sert a rien :(
                        go.name = s[0]; //a voir
                        GameData.currentState.map.SetEntity(GameData.blockLibrary.blocks[s[0]], pos);
                    }
                    sr.Close();
                }
            } catch (Exception e) {
                Debug.LogError("The file could not be read:");
                Debug.LogError(e.Message);
            }
        }
    }
}
