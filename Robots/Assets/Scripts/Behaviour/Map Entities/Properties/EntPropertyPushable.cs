using UnityEngine;

public class EntPropertyPushable : EntProperty {
	[SerializeField]
	private int _strongOfPush = 1;

	protected override void _Interact(EntityEvent actionType, MapEntity entity) {
		if (actionType == EntityEvent.Push) {
			var posEntityPusher = GameData.currentState.map.ToLocalPos(entity.tr.position);
			var posEntityPush = GameData.currentState.map.ToLocalPos(gameObject.transform.position);
			MapDirection direction;

			if (posEntityPush.x != posEntityPusher.x &&
				posEntityPush.y == posEntityPusher.y &&
				posEntityPush.y == posEntityPusher.y) {
				direction = posEntityPusher.x > posEntityPush.x ? MapDirection.left : MapDirection.right;
			} else if (posEntityPush.x == posEntityPusher.x &&
						 posEntityPush.y != posEntityPusher.y &&
						 posEntityPush.y == posEntityPusher.y) {
				direction = posEntityPusher.y > posEntityPush.y ? MapDirection.down : MapDirection.up;
			} else if (posEntityPush.x == posEntityPusher.x &&
						 posEntityPush.y == posEntityPusher.y &&
						 posEntityPush.y != posEntityPusher.y) {
				direction = posEntityPusher.z > posEntityPush.z ? MapDirection.back : MapDirection.forward;
			} else {
				Debug.Log("Error in EntPropertyPushable : Can't push");
				direction = MapDirection.zero;
			}

			GameData.currentState.map.GetEntity(posEntityPush)
				.Move(PosToMove(entity, direction, _strongOfPush));
		}
	}

	public MapDirection DirectionToMove(MapEntity entity) {
		return 
	}

	public MapPosition PosToMove(MapEntity entity, MapDirection direction, int strong) {
		return GameData.currentState.map.ToLocalPos(entity.transform.position) + direction*strong;
	}

	public int getStrongOfPush() {
		return _strongOfPush;
	}
}