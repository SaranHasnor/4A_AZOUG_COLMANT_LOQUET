using UnityEngine;

public class EntPropertyPushable : EntProperty {
	[SerializeField]
	private int _strongOfPush = 1;

	protected override void _Interact(EntityEvent actionType, MapEntity entity) {
		if (actionType == EntityEvent.Push) {
			var posEntityPush = GameData.currentState.map.ToLocalPos(gameObject.transform.position);

			GameData.currentState.map.GetEntity(posEntityPush)
				.Move(PosToMove(entity, MapDirection.DirectionToMove(posEntityPush, GameData.currentState.map.ToLocalPos(entity.tr.position)), _strongOfPush));
		}
	}

	public MapPosition PosToMove(MapEntity entity, MapDirection direction, int strong) {
		return GameData.currentState.map.ToLocalPos(entity.transform.position) + direction*strong;
	}

	public int getStrongOfPush() {
		return _strongOfPush;
	}
}