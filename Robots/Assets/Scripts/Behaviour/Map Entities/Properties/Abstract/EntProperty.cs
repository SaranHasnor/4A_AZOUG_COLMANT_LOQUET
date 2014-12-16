using UnityEngine;

public enum EntityEvent : int {
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

    protected abstract void _Interact(EntityEvent actionType, params Object[] args);

    private void Interact(EntityEvent actionType, params Object[] args) {
        try {
            _Interact(actionType, args);
        } catch (System.Exception e) {
            Debug.LogException(e);
        }
    }
    
    public void AddListener(MapEntity actuator) {
        actuator.OnEntityInteraction += Interact;
    }

    public void RemoveListener(MapEntity actuator) {
        actuator.OnEntityInteraction -= Interact;
    }
}