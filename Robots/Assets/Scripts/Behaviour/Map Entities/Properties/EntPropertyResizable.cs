using UnityEngine;

public class EntPropertyResizable : EntProperty {
	protected override void _Interact(EntityEvent actionType, MapEntity entity) {
		if (actionType == EntityEvent.Resize) {
			// TODO : Completer algo property Resize
			throw new System.ArgumentException(System.String.Format("L'event {0} a été levé mais l'implémentation n'est pas terminé.", actionType));
        }
    }
}