using UnityEngine;

public enum ActionOnBlock : int {
    Move = 0
};

/// <summary>
///     <c>BlocProperty</c> is abstract class.
///     Subclasses of this class can implement a variety of event functions, so this class is merely a "marker" for subclasses
/// </summary>
public abstract class BlockProperty : MonoBehaviour {

    protected abstract EntityActionResult _Interact(ActionOnBlock action);

    private EntityActionResult Interact(ActionOnBlock action) {
        try {
            return this._Interact(action);
        } catch (ActionException e) {
            Debug.LogException(e);
            return EntityActionResult.Error;
        }
    }

    public void AddListener(BlockScript actuator) {
        actuator.OnBlockInteract += this.Interact;
    }

    public void RemoveListener(BlockScript actuator) {
        actuator.OnBlockInteract -= this.Interact;
    }
}