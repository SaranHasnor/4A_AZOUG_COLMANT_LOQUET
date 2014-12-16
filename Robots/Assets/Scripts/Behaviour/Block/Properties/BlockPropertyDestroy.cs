public class BlockPropertyDestructible : BlockProperty {
    protected override void _Interact(ActionOnBlock action, string[] args = null) {
        if (action != ActionOnBlock.Destroy) {
            // TODO : Completer algo property Destroy
        }
    }
}