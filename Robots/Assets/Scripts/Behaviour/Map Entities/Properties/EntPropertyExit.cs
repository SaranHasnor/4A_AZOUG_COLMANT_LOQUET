using UnityEngine;

public class EntPropertyExit : EntProperty {
    [SerializeField]
    private uint _necessaryNbOfBot;

    protected override void _Interact(EntityEvent action, MapEntity entity) {
        if (action == EntityEvent.Exit) {
            // TODO : Completer algo property Exit
        }
    }
}