using UnityEngine;

public class EntPropertyRotatable : EntProperty {
    protected override void _Interact(EntityEvent action, params Object[] args) {
        if (action == EntityEvent.Rotate) {
            gameObject.transform.Rotate(Vector3.up, 90f);
        }
    }
}