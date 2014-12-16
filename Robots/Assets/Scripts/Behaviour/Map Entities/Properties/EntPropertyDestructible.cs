using UnityEngine;

public class EntPropertyDestructible : EntProperty {
    protected override void _Interact(ActionOnBlock action, params Object[] args) {
        if (action == ActionOnBlock.Destroy) {
            // TODO : Completer algo property Destroy
        }
    }
}