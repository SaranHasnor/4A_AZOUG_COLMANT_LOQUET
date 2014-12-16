using UnityEngine;

public class EntPropertyUseGravity : EntProperty
{
    [SerializeField]
    public int FallingSpeed;

    protected override void _Interact(ActionOnBlock action, params Object[] args) {
        if (action == ActionOnBlock.Fall) {
            var posEntityPush = gameObject.transform.position;
            GameData.currentState.map.GetEntity(posEntityPush)
                .Move(new Vector3(  gameObject.transform.position.x,
                                    gameObject.transform.position.y - FallingSpeed,
                                    gameObject.transform.position.z ));
        }
    }
}