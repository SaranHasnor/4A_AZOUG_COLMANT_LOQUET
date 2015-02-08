using UnityEngine;

public class EntPropertyImmaterial : EntProperty {
    protected override void _Interact(EntityEvent actionType, MapEntity entity) {
		if (actionType == EntityEvent.Collide) {
			// TODO : Completer algo property Solid
			throw new System.ArgumentException(System.String.Format("L'event {0} a été levé mais l'implémentation n'est pas terminé.", actionType));
        }
    }
}