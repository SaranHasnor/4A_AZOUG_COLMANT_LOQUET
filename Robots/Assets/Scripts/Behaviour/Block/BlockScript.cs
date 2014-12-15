using UnityEngine;

public class BlockScript : MapEntity {
    [SerializeField]
    private GameObject _logic;

    private BlockProperty[] _properties;

    // Should have a list of properties that can be attached to it
    // The only argument for making this a runnable entity is to update their state each turn
    // Maybe we could also have blocks take actions at specific turns, but I'd like this to be a property instead

    void Start() {
        _properties = _logic.GetComponents<BlockProperty>();
    }

    public EntityActionResult Interact(ActionOnBlock action, string[] args = null) {
        var result = EntityActionResult.Error;
        foreach (var property in _properties) {
            var tmp = property.Interact(action, args);
            if (tmp != EntityActionResult.Error) {
                result = tmp;
                break;
            }
        }

        return result;
    }
}
