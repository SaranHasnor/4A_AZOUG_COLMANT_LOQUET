using UnityEngine;

public class EntPropertyUsesGravity : EntProperty
{
    [SerializeField]
    private int _fallingSpeed;

    protected override void _Interact(EntityEvent action, MapEntity entity) {
        if (action == EntityEvent.Fall) {
            var posEntityPush = gameObject.transform.position;
            GameData.currentState.map.GetEntity(posEntityPush)
                .Move(new Vector3(  gameObject.transform.position.x,
                                    gameObject.transform.position.y - _fallingSpeed,
                                    gameObject.transform.position.z ));
        }
    }
}