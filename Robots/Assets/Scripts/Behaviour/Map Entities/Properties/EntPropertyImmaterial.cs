using UnityEngine;

public class EntPropertyImmaterial : EntProperty {
    protected override void _Interact(EntityEvent actionType, MapEntity entity) {
		if (actionType == EntityEvent.Collide) {
            // TODO : Completer algo property Solid
        }
    }
}