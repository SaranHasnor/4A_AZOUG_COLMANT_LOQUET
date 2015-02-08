using UnityEngine;

public class EntPropertyDestructible : EntProperty {
	protected override void _Interact(EntityEvent actionType, MapEntity entity) {
		if (actionType == EntityEvent.Break) {
			// TODO : Completer algo property Breakable
			throw new System.ArgumentException(System.String.Format("L'event {0} a été levé mais l'implémentation n'est pas terminé.", actionType));
			
			// GameData.currentState.map.GetEntity(entity).Destroy();
        }
    }
}