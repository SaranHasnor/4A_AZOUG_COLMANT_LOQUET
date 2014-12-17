using UnityEngine;

public class EntPropertyTeleporter : EntProperty {
    protected override void _Interact(EntityEvent action, MapEntity entity) {
        if (action == EntityEvent.Teleport) {
            // TODO : Completer algo property Teleporter
        }
    }
}