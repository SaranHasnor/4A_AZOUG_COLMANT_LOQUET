using UnityEngine;

public class EntPropertyPushable : EntProperty {
	[SerializeField]
	private int _strongOfPush = 1;

	protected override void _Interact(EntityEvent actionType, MapEntity entity) {
		if (actionType == EntityEvent.Push)
		{
			MapPosition posEntityPush = owner.localPosition;

			owner.Push(MapDirection.DirectionToMove(entity.localPosition, posEntityPush), _strongOfPush);
		}
	}

	public int getStrongOfPush() {
		return _strongOfPush;
	}
}