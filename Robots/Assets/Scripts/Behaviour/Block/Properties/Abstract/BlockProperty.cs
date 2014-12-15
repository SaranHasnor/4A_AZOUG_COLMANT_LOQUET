using UnityEngine;

public enum ActionOnBlock : int {
    Move = 0
};

/// <summary>
///     <c>BlocProperty</c> is abstract class.
///     Subclasses of this class can implement a variety of event functions, so this class is merely a "marker" for subclasses
/// </summary>
public abstract class BlockProperty : MonoBehaviour {

    protected abstract EntityActionResult _Interact(ActionOnBlock action, string[] args = null);

    public EntityActionResult Interact(ActionOnBlock action, string[] args = null) {
        try {
            return this._Interact(action, args);
        } catch (ActionException e) {
            Debug.LogException(e);
            return EntityActionResult.Error;
        }
    }
}