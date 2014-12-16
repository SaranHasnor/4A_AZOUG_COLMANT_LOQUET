using UnityEngine;

public delegate void BlockInteract(ActionOnBlock action, params Object[] args);

public class BlockScript : MapEntity {
    [SerializeField]
    private GameObject _logic;

    private BlockProperty[] _properties;
    public event BlockInteract OnBlockInteract;

    // Should have a list of properties that can be attached to it
    // The only argument for making this a runnable entity is to update their state each turn
    // Maybe we could also have blocks take actions at specific turns, but I'd like this to be a property instead

    void Start() {
        _properties = _logic.GetComponents<BlockProperty>();
        foreach (var property in _properties) {
            property.AddListener(this);
        }
    }

    public void Interact(ActionOnBlock action, params Object[] args) {
        if (OnBlockInteract != null)
            OnBlockInteract(action, args);
    }
}
