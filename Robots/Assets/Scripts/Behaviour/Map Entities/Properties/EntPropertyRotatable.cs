using UnityEngine;

public class EntPropertyRotatable : EntProperty {
	protected override void _Interact(EntityEvent actionType, MapEntity entity) {
		if (actionType == EntityEvent.Rotate) {
            gameObject.transform.Rotate(Vector3.up, 90f);
        }
    }
}