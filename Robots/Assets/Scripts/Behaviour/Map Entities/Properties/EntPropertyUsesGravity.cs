using UnityEngine;

public class EntPropertyUseGravity : EntProperty
{
    [SerializeField]
    public int FallingSpeed;

    protected override void _Interact(EntityEvent action, params Object[] args) {
        if (action == EntityEvent.Fall) {
            var posEntityPush = gameObject.transform.position;
            GameData.currentState.map.GetEntity(posEntityPush)
                .Move(new Vector3(  gameObject.transform.position.x,
                                    gameObject.transform.position.y - FallingSpeed,
                                    gameObject.transform.position.z ));
        }
    }
}