using UnityEngine;

public class EntPropertyExit : EntProperty {
	[SerializeField]
	private uint _necessaryNbOfBot;

	protected override void _Interact(EntityEvent action, MapEntity entity) {
		if (action == EntityEvent.Exit || action == EntityEvent.CollisionEnter) {
			entity.Destroy();
			--_necessaryNbOfBot;
			if (_necessaryNbOfBot <= 0) {
				// TODO : Call end
				// Porte completed.
			}
		}
	}
}