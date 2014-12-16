using UnityEngine;
using System.Collections;
using System.Diagnostics;

public abstract class MapEntity : MonoBehaviour
{ // Describes an entity that has a physical presence on the map
	public int player; // Player who controls this entity, none if < 0
    [SerializeField] public Transform tr;
    [SerializeField] public GameObject go;
    public static int count;
    // TODO: Code for moving the object on the scene, disabling it, re-enabling it, etc

    public int Move(Vector3 pos)
    {
        return GameData.currentState.map.MoveEntity(this, pos);
    }

    public void Destroy()
    {
        GameData.currentState.map.DeleteEntity(tr.position);
    }
}
