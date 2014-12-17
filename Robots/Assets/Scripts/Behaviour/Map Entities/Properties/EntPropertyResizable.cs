using UnityEngine;

public class EntPropertyResizable : EntProperty {
    protected override void _Interact(EntityEvent action, MapEntity entity) {
        if (action == EntityEvent.Resize) {
            // TODO : Completer algo property Resize
        }
    }
}