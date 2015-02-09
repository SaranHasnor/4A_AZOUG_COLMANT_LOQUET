using UnityEngine;

public class EntPropertyPushable : EntProperty {
	[SerializeField]
	private int _strongOfPush = 1;

	protected override void _Interact(EntityEvent actionType, MapEntity entity) {
		if (actionType == EntityEvent.Push)
		{
			var posEntityPush = owner.localPosition;

			GameData.currentState.map.GetEntity(posEntityPush)
				.Push(MapDirection.DirectionToMove(posEntityPush, entity.localPosition), _strongOfPush);
		}
	}

	public MapPosition PosToMove(MapEntity entity, MapDirection direction, int strong) {
		return GameData.currentState.map.ToLocalPos(entity.transform.position) + direction*strong;
	}

	public int getStrongOfPush() {
		return _strongOfPush;
	}
}