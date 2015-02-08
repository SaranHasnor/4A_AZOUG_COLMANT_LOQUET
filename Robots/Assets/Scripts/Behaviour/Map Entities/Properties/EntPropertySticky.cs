using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntPropertySticky : EntProperty {
	[SerializeField]
	private uint _nbMaxSticky = 10;

	protected override void _Interact(EntityEvent actionType, MapEntity entity) {
		if (actionType == EntityEvent.Push)
		{
			// TODO : Completer algo property Stick
			throw new ArgumentException(String.Format("L'event {0} a été levé mais l'implémentation n'est pas terminé.", actionType));

			// Check that this entity can be pushed.
			var propPusable = entity.gameObject.GetComponents<EntPropertyPushable>(); // TODO Amau : may be change by entity.properties
			if (propPusable == null)
				return;

			// Get all related entities. 
			var nbMaxSticky = _nbMaxSticky;
			var neighboursSticky = GetAllNeighbourRecur(entity, 0, ref nbMaxSticky);

			// Check that can interact with all entities
			if (neighboursSticky.Count >= nbMaxSticky)
				return;


			//foreach (var neighbourSticky in neighboursSticky)
			//	neighbourSticky.CanMove();
		}
    }

	private List<MapEntity> GetAllNeighbourRecur(MapEntity entity, uint nbRecurs, ref uint nbMaxSticky) {
		var neighbours = new List<MapEntity>(GameData.currentState.map.GetAllNeighbour(entity).Values.ToList());
		var listRetour = new List<MapEntity>(neighbours);
		nbMaxSticky = nbMaxSticky <= _nbMaxSticky ? nbMaxSticky : _nbMaxSticky;

		if (nbRecurs >= nbMaxSticky)
			return listRetour;

		// TODO Amau : check that neighbour is not static
		foreach (var neighbour in neighbours
			.Where(neighbour => neighbour != null)
			.Where(neighbour => neighbour.gameObject.GetComponents<EntPropertyPushable>() != null)
			.Where(neighbour => neighbour.gameObject.GetComponents<EntPropertyImmaterial>() == null)) {  // TODO Amau : may be change by entity.properties
			foreach (var newNeighbour in GetAllNeighbourRecur(neighbour, nbRecurs++, ref nbMaxSticky)
				.Where(newNeighbour => !listRetour.Contains(newNeighbour))) {
				listRetour.Add(newNeighbour);
			}
		}
		return listRetour;
	}
}