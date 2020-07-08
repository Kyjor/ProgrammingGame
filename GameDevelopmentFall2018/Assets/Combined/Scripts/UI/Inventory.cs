using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour {

    public Text saveName;

    public InputField SaveInput;
    int scriptID = 0;
    private Dictionary<string, int> inventory;
    void Start()
    {
        inventory = new Dictionary<string, int>();
    }

    public void AddOrEdit()
    {
        if (!inventory.ContainsKey(SaveInput.text))
        {
            inventory.Add(SaveInput.text, scriptID);
            scriptID = scriptID + 1;
            Debug.Log(inventory);
        }
        else
        {
            inventory[SaveInput.text] = scriptID;
            Debug.Log(inventory);
        }
    }

    //public string GetScript(string stringScript)
    //{
    //    if (inventory.ContainsKey(stringScript))
    //    {
    //        return inventory[stringScript];
    //    }
    //    else
    //    {
    //        return null;
    //    }
    //}

    public Dictionary<string,int> GetInventory()
    {
        Debug.Log(inventory);
        return inventory;
    }
}
