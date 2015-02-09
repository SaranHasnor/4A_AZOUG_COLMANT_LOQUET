using UnityEngine;

public class EntPropertyPushable : EntProperty {
	[SerializeField]
	private int _strongOfPush = 1;

	protected override void _Interact(EntityEvent actionType, MapEntity entity) {
		if (actionType == EntityEvent.Push) {
			GameData.currentState.map.GetEntity(owner.localPosition)
				.Push(MapDirection.DirectionToMove(owner.localPosition, entity.localPosition), _strongOfPush);
		}
	}
}