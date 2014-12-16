using UnityEngine;

public enum ActionOnBlock : int {
    Move = 0,
    Destroy,
    Gravitate,
    Resize,
    Rotate,
    Solid,
    Stick
};

/// <summary>
///     <c>BlocProperty</c> is abstract class.
///     Subclasses of this class can implement a variety of event functions, so this class is merely a "marker" for subclasses
/// </summary>
public abstract class BlockProperty : MonoBehaviour {

    protected abstract void _Interact(ActionOnBlock action, string[] args = null);

    private void Interact(ActionOnBlock action, string[] args = null) {
        try {
            _Interact(action, args);
        } catch (ActionException e) {
            Debug.LogException(e);
        }
    }

    public void AddListener(BlockScript actuator) {
        actuator.OnBlockInteract += Interact;
    }

    public void RemoveListener(BlockScript actuator) {
        actuator.OnBlockInteract -= Interact;
    }
}