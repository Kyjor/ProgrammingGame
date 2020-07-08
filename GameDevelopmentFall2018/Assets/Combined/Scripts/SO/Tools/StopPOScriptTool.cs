using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tools/StopPoScriptTool")]
public class StopPOScriptTool : BaseTool {

    public override void OnUseDown(Player player)
    {
        ProgrammableObject ob = player.pRay.getFirstPO();
        if (ob)
            ob.fc.stopCoroutine();
    }
}
