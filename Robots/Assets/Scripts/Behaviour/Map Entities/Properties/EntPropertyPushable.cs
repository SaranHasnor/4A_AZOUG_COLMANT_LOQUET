using UnityEngine;

public class EntPropertyPushable : EntProperty {
    [SerializeField]
    private int _strongOfPush = 1;

    protected override void _Interact(EntityEvent action, MapEntity entity) {
        if (action == EntityEvent.Move) {
            var posEntityPusher = GameData.currentState.map.GetEntity(entity).transform.position;
            var posEntityPush = gameObject.transform.position;

            if (posEntityPush.x != posEntityPusher.x &&
                posEntityPush.y == posEntityPusher.y &&
                posEntityPush.y == posEntityPusher.y) {
                var tmp = posEntityPusher.x > posEntityPush.x ? -_strongOfPush : _strongOfPush;
                GameData.currentState.map.GetEntity(posEntityPush)
                    .Move(Map.GetLocalPos(new Vector3(posEntityPush.x + tmp, posEntityPush.y, posEntityPush.z)));
            } else if (posEntityPush.x == posEntityPusher.x &&
                       posEntityPush.y != posEntityPusher.y &&
                       posEntityPush.y == posEntityPusher.y) {
                var tmp = posEntityPusher.y > posEntityPush.y ? -_strongOfPush : _strongOfPush;
                GameData.currentState.map.GetEntity(posEntityPush)
                    .Move(Map.GetLocalPos(new Vector3(posEntityPush.x, posEntityPush.y + tmp, posEntityPush.z)));
            } else if (posEntityPush.x == posEntityPusher.x &&
                       posEntityPush.y == posEntityPusher.y &&
                       posEntityPush.y != posEntityPusher.y) {
                var tmp = posEntityPusher.z > posEntityPush.z ? -_strongOfPush : _strongOfPush;
                GameData.currentState.map.GetEntity(posEntityPush)
                    .Move(Map.GetLocalPos(new Vector3(posEntityPush.x, posEntityPush.y, posEntityPush.z + tmp)));
            } else {
				Debug.Log("Error in EntPropertyPushable : Can't push");
            }

        }
    }
}