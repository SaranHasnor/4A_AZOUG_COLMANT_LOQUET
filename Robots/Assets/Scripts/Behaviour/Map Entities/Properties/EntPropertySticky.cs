using UnityEngine;

public class EntPropertySticky : EntProperty {
    protected override void _Interact(EntityEvent action, params Object[] args) {
        if (action == EntityEvent.Stick) {
            // TODO : Completer algo property Stick
        }
    }
}