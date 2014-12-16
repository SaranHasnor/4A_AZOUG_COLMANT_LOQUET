using UnityEngine;

public class EntPropertyResizable : EntProperty {
    protected override void _Interact(EntityEvent action, params Object[] args) {
        if (action == EntityEvent.Resize) {
            // TODO : Completer algo property Resize
        }
    }
}