using UnityEngine;

public class EntPropertyExit : EntProperty {
	[SerializeField]
	private uint _necessaryNbOfBot;

	protected override void _Interact(EntityEvent action, MapEntity entity) {
		if (action == EntityEvent.Exit || action == EntityEvent.CollisionEnter) {
			entity.Destroy();
			--_necessaryNbOfBot;

			if (_necessaryNbOfBot <= 0) {
				// TODO : Lock Porte
				// Porte completed.
				// this.RemoveListener();
				// this.activate = false;
			}
		}
	}
}