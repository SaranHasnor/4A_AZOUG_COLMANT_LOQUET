using UnityEngine;

public class EntPropertyWalkableSurface : EntProperty {
	protected override void _Interact(EntityEvent actionType, MapEntity entity) {
		if (actionType == EntityEvent.StepOn) {
			throw new System.ArgumentException(System.String.Format("L'event {0} a été levé mais l'implémentation n'est pas terminé.", actionType));
		} else if (actionType == EntityEvent.StepOff) {
			throw new System.ArgumentException(System.String.Format("L'event {0} a été levé mais l'implémentation n'est pas terminé.", actionType));
		}
	}
}