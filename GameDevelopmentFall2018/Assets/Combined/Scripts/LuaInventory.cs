using System.Collections.Generic;

public class LuaInventory{
    public LuaScript currentScript;
    public List<LuaScript> inventory;

    //void on awake load all inventories from path. use the save and load.

    public LuaInventory()
    {
        inventory = new List<LuaScript>();
    }

    //adds or edits a lua script to this inventory.
    public void AddOrEdit(LuaScript script)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].scriptName == script.scriptName)
            {
                inventory[i].scriptText = script.scriptText;
                return;
            }
        }
        inventory.Add(script);
    }

    public LuaScript GetScript(string stringKey)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].scriptName == stringKey)
            {
                return inventory[i];
            }
        }
        return null;
    }
}
