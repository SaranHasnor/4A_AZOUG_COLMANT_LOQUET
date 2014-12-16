using UnityEngine;

public class BlockPropertyDestructible : BlockProperty {
    protected override void _Interact(ActionOnBlock action, params Object[] args) {
        if (action == ActionOnBlock.Destroy) {
            // TODO : Completer algo property Destroy
        }
    }
}