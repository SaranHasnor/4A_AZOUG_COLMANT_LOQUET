using UnityEngine;

public delegate EntityActionResult BlockInteract(ActionOnBlock action);

public class BlockScript : MonoBehaviour // RunnableEntity?
{
    [SerializeField]
    private GameObject _logic;

    public event BlockInteract OnBlockInteract;

    private BlockProperty[] _properties;

    // Should have a list of properties that can be attached to it
    // The only argument for making this a runnable entity is to update their state each turn
    // Maybe we could also have blocks take actions at specific turns, but I'd like this to be a property instead

    void Start() {
        _properties = _logic.GetComponents<BlockProperty>();
        foreach (var property in _properties) {
            property.AddListener(this);
        }
    }

    public EntityActionResult Interact(ActionOnBlock action) {
        var result = EntityActionResult.Error;
        if (OnBlockInteract != null)
            result = OnBlockInteract(action);

        return result;
    }
}
