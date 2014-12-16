using UnityEngine;

public class BlockPropertyRotate : BlockProperty {
    protected override void _Interact(ActionOnBlock action, params Object[] args) {
        if (action == ActionOnBlock.Rotate) {
            gameObject.transform.Rotate(Vector3.up, 90f);
        }
    }
}