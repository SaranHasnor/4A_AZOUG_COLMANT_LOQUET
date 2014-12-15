using UnityEngine;
using System.Collections;

public class BlockPropertyMove : BlockProperty {
    protected override EntityActionResult _Interact(ActionOnBlock action, string[] args = null) {
        EntityActionResult result;

        if (action != ActionOnBlock.Move) {
            result = EntityActionResult.Failure;
        } else {
            result = EntityActionResult.Success;
        }

        return result;
    }
}
