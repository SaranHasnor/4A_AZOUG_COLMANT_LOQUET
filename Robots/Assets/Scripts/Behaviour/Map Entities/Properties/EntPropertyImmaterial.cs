using UnityEngine;

public class EntPropertyImmaterial : EntProperty {
    protected override void _Interact(EntityEvent action, MapEntity entity) {
        if (action == EntityEvent.Solid) {
            // TODO : Completer algo property Solid
        }
    }
}