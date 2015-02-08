using UnityEngine;

public class EntPropertyDestructible : EntProperty {
	protected override void _Interact(EntityEvent actionType, MapEntity entity) {
		if (actionType == EntityEvent.Destroy) {
			GameData.currentState.map.GetEntity(entity).Destroy();
        }
    }
}