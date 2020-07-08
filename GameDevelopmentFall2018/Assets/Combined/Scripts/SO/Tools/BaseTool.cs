using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(menuName ="Tools/Basetool")]
public class BaseTool : ScriptableObject
{
    
    public string toolName = "BaseTool";
    public virtual void OnSwitchIn(Player player) { }
    public virtual void OnSwitchOut(Player player) { }

    public virtual void ToolUpdate(Player player) { }
    public virtual void ToolFixedUpdate(Player player) { }

    public virtual void OnUseDown(Player player) { }
    public virtual void OnUseHold(Player player) { }
    public virtual void OnUseUp(Player player) { }

    public virtual void OnUseDownFixed(Player player) { }
    public virtual void OnUseHoldFixed(Player player) { }
    public virtual void OnUseUpFixed(Player player) { }
}