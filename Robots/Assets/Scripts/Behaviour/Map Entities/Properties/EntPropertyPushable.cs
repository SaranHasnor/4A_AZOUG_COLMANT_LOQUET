using UnityEngine;

public class EntPropertyPushable : EntProperty {
	[SerializeField]
	private int _strongOfPush = 1;

	protected override void _Interact(EntityEvent actionType, MapEntity entity) {
		if (actionType == EntityEvent.Push) {
			owner.Push(MapDirection.DirectionToMove(entity.localPosition, owner.localPosition), _strongOfPush);
		}
	}
}