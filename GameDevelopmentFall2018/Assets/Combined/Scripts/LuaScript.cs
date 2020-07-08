using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuaScript {

    public LuaScript()
    {
        scriptName = "Empty";
        scriptText = "--EmptyScript";
    }
    public LuaScript(string sName, string sText)
    {
        scriptName = sName;
        scriptText = sText;
    }
    public string scriptName;
    public string scriptText;
}
