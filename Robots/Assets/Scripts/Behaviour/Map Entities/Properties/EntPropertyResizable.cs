using UnityEngine;

public class EntPropertyResizable : EntProperty {
	protected override void _Interact(EntityEvent actionType, MapEntity entity) {
		if (actionType == EntityEvent.Resize) {
            // TODO : Completer algo property Resize
        }
    }
}