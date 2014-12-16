using UnityEngine;

public class EntPropertyImmaterial : EntProperty {
    protected override void _Interact(ActionOnBlock action, params Object[] args) {
        if (action == ActionOnBlock.Solid) {
            // TODO : Completer algo property Solid
        }
    }
}