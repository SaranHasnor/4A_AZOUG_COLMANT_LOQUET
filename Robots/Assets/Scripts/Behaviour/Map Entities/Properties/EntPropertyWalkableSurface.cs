using UnityEngine;

public class EntPropertyWalkableSurface : EntProperty {
	protected override void _Interact(EntityEvent actionType, MapEntity entity) {
		if (actionType == EntityEvent.StepOn) {
			GameData.currentState.map.GetEntity(entity).Destroy();
		} else if (actionType == EntityEvent.StepOff) {
			
		}
	}
}