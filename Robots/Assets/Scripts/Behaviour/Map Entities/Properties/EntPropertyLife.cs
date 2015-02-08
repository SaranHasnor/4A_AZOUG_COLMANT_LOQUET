using UnityEngine;

public class EntPropertyLife : EntProperty {
	protected override void _Interact(EntityEvent actionType, MapEntity entity) {
		if (actionType == EntityEvent.Damage) {
			// TODO : Completer algo property Life
			throw new System.ArgumentException(System.String.Format("L'event {0} a été levé mais l'implémentation n'est pas terminé.", actionType));
		}
	}
}