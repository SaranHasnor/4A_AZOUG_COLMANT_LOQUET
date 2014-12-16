using UnityEngine;

public class EntPropertyDestructible : EntProperty {
    protected override void _Interact(EntityEvent action, params Object[] args) {
        if (action == EntityEvent.Destroy) {
            // TODO : Completer algo property Destroy
        }
    }
}