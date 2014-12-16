using UnityEngine;

public class BlockPropertySolid : BlockProperty {
    protected override void _Interact(ActionOnBlock action, params Object[] args) {
        if (action == ActionOnBlock.Solid) {
            // TODO : Completer algo property Solid
        }
    }
}