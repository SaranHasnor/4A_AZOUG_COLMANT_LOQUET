using UnityEngine;

public class EntPropertyExit : EntProperty {
    [SerializeField]
    private uint _necessaryNbOfBot;

    protected override void _Interact(EntityEvent action, params Object[] args) {
        if (action == EntityEvent.Exit) {
            // TODO : Completer algo property Exit
        }
    }
}