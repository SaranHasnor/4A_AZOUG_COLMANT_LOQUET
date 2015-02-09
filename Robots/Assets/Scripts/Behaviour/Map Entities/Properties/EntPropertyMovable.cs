using UnityEngine;

public class EntPropertyMovable : EntProperty {
	protected override void _Interact(EntityEvent actionType, MapEntity entity) {
		if (actionType == EntityEvent.Move) {
			throw new System.NotImplementedException(System.String.Format("L'event {0} a été levé mais l'implémentation n'est pas terminé.", actionType));
		}
	}
}