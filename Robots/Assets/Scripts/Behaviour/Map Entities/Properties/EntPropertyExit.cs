using UnityEngine;

public class EntPropertyExit : EntProperty {
	[SerializeField] 
	private uint _necessaryNbOfBot;

	protected override void _Interact(EntityEvent action, MapEntity entity) {
		if (action == EntityEvent.Collide) {
			entity.Destroy();
			--_necessaryNbOfBot;
			GameData.gameMaster.SetExitBot();
			if (_necessaryNbOfBot <= 0) {
				// Porte completed.
				RemoveListener(_owner);
				enabled=!enabled;
			}
		}
	}
}