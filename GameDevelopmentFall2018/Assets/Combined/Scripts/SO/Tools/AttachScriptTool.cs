using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tools/AttachScriptTool")]
class AttachScriptTool : BaseTool
{

    public AttachScriptTool()
    {
        toolName = "Attach Script Tool";
    }

    public override void OnUseDown(Player player)
    {
        ProgrammableObject temp = player.pRay.getFirstPO();
        if (temp != null)
        {
            temp.luaScript = player.luaInventory.currentScript;
        }
    }
}
