using UnityEngine;

public class EntPropertyTeleporter : EntProperty {
    protected override void _Interact(EntityEvent action, params Object[] args) {
        if (action == EntityEvent.Teleport) {
            // TODO : Completer algo property Teleporter
        }
    }
}