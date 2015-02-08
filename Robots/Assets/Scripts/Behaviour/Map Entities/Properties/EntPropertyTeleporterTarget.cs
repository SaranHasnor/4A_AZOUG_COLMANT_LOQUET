﻿using UnityEngine;

public class EntPropertyTeleporterTarget : EntProperty {
	protected override void _Interact(EntityEvent actionType, MapEntity entity) {
		// TODO : Completer algo property Teleporter Target
		throw new System.ArgumentException(System.String.Format("L'event {0} a été levé mais l'implémentation n'est pas terminé.", actionType));
	}
}