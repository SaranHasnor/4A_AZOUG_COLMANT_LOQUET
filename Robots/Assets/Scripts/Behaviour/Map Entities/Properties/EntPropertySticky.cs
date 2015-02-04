using UnityEngine;

public class EntPropertySticky : EntProperty {
	protected override void _Interact(EntityEvent actionType, MapEntity entity) {
		if (actionType == EntityEvent.Move || actionType == EntityEvent.Push) {
            // TODO : Completer algo property Stick
        }
    }
}