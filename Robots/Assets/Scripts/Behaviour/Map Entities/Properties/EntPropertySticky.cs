using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntPropertySticky : EntProperty {
	[SerializeField]
	private uint _nbMaxSticky = 10;

	protected override void _Interact(EntityEvent actionType, MapEntity entity) {

		if (actionType == EntityEvent.Move || actionType == EntityEvent.Push) {
			throw new ArgumentException(String.Format("L'event {0} a été levé mais l'implémentation n'est pas terminé.", actionType));
		}
    }

	private List<MapEntity> GetAllNeighbourRecur(MapEntity entity, uint nbRecurs, uint nbMaxSticky) {
		var neighbours = new List<MapEntity>(GameData.currentState.map.GetAllNeighbour(entity).Values.ToList());
		var listRetour = new List<MapEntity>(neighbours);

		foreach (var neighbour in neighbours.Where(neighbour => neighbour!= null)) {
			foreach (var newNeighbour in GetAllNeighbourRecur(neighbour, nbRecurs++, nbMaxSticky).Where(newNeighbour => !listRetour.Contains(newNeighbour))) {
				if ()
					listRetour.Add(newNeighbour);
			}
		}
		return listRetour;
	}
}