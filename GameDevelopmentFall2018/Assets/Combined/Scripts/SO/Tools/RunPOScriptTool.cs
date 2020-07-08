using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tools/RunPoScriptTool")]
public class RunPOScriptTool : BaseTool {

    public override void OnUseDown(Player player)
    {
        ProgrammableObject ob = player.pRay.getFirstPO();
        if (ob)
            ob.RunScript();
    }
}
