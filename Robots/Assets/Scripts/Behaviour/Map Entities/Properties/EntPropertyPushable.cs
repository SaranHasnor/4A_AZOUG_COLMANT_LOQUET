using UnityEngine;

public class EntPropertyPushable : EntProperty {
    [SerializeField]
    public int StrongOfPush = 1;

    protected override void _Interact(ActionOnBlock action, params Object[] args) {
        if (action == ActionOnBlock.Move) {
            var posEntityPusher = GameData.currentState.map.GetEntity((MapEntity)args[0]).transform.position;
            var posEntityPush = gameObject.transform.position;

            if (posEntityPush.x != posEntityPusher.x &&
                posEntityPush.y == posEntityPusher.y &&
                posEntityPush.y == posEntityPusher.y) {
                var tmp = posEntityPusher.x > posEntityPush.x ? -StrongOfPush : StrongOfPush;
                GameData.currentState.map.GetEntity(posEntityPush)
                    .Move(new Vector3(posEntityPush.x + tmp, posEntityPush.y, posEntityPush.z));
            } else if (posEntityPush.x == posEntityPusher.x &&
                       posEntityPush.y != posEntityPusher.y &&
                       posEntityPush.y == posEntityPusher.y) {
                var tmp = posEntityPusher.y > posEntityPush.y ? -StrongOfPush : StrongOfPush;
                GameData.currentState.map.GetEntity(posEntityPush)
                    .Move(new Vector3(posEntityPush.x, posEntityPush.y + tmp, posEntityPush.z));
            } else if (posEntityPush.x == posEntityPusher.x &&
                       posEntityPush.y == posEntityPusher.y &&
                       posEntityPush.y != posEntityPusher.y) {
                var tmp = posEntityPusher.z > posEntityPush.z ? -StrongOfPush : StrongOfPush;
                GameData.currentState.map.GetEntity(posEntityPush)
                    .Move(new Vector3(posEntityPush.x, posEntityPush.y, posEntityPush.z + tmp));
            } else {
                Debug.Log("Error in BlockPropertyMove : Can't push");
            }

        }
    }
}