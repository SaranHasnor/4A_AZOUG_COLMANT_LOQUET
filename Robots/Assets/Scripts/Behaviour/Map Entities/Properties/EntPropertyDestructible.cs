using UnityEngine;

public class EntPropertyDestructible : EntProperty {
    protected override void _Interact(EntityEvent action, MapEntity entity) {
        if (action == EntityEvent.Destroy) {
            // TODO : Completer algo property Destroy
        }
    }
}