using UnityEngine;

public class EntPropertyUsesGravity : EntProperty
{
    [SerializeField]
    private int _fallingSpeed;

	protected override void _Interact(EntityEvent actionType, MapEntity entity) {
		if (actionType == EntityEvent.Fall) {
			GameData.currentState.map.GetEntity(gameObject.transform.position)
				.Move(GameData.currentState.map.ToLocalPos(new Vector3(	gameObject.transform.position.x,
																		gameObject.transform.position.y - _fallingSpeed,
																		gameObject.transform.position.z
																		)));
        }
    }
}