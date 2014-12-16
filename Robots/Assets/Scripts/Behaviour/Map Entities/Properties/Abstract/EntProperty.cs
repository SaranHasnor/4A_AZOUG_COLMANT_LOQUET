using UnityEngine;

public enum ActionOnBlock : int {
    Move = 0,
    Destroy,
    Fall,
    Resize,
    Rotate,
    Solid,
    Stick
};

/// <summary>
///     <c>BlocProperty</c> is abstract class.
///     Subclasses of this class can implement a variety of event functions, so this class is merely a "marker" for subclasses
/// </summary>
public abstract class EntProperty : MonoBehaviour {

    protected abstract void _Interact(ActionOnBlock action, params Object[] args);

    private void Interact(ActionOnBlock action, params Object[] args) {
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