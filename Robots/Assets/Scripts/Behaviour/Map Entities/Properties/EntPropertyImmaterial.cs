using UnityEngine;

public class EntPropertyImmaterial : EntProperty {
    protected override void _Interact(EntityEvent action, params Object[] args) {
        if (action == EntityEvent.Solid) {
            // TODO : Completer algo property Solid
        }
    }
}