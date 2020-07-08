using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonScript : MonoBehaviour
{
    public Player player;
    public Button InitButton;
    public InputField SaveInput;
    // public int i = 0;
    public GameObject Base;
    public InputField DeleteInput;
    public GameObject parent;


    List<GameObject> Buttons = new List<GameObject>();

    // string ext = ".lua";
    //  string path = "Assets/Resources/LuaFiles/";
    public InputField ScriptInput;

    public void Start()
    {
        UnityEngine.Object[] FileListing = Resources.LoadAll("LuaFiles", typeof(TextAsset));
        foreach (UnityEngine.Object temp in FileListing)
        {
            TextAsset txt = (TextAsset)temp;
            ButtonMake(temp.name);
            player.luaInventory.inventory.Add(new LuaScript(temp.name, txt.text));
        }
        
        if (player.luaInventory.inventory.Count !=0)
            player.luaInventory.currentScript = player.luaInventory.inventory[0];
    }

    public void overWrite()
    {
        string scriptText = ScriptInput.text;
        File.Delete(Path.path + SaveInput.text + Path.ext);
        var sWriter = File.Open(Path.path + SaveInput.text + Path.ext, FileMode.OpenOrCreate);

        sWriter.Write(System.Text.Encoding.UTF8.GetBytes(ScriptInput.text), 0, System.Text.Encoding.UTF8.GetByteCount(ScriptInput.text));
        sWriter.Close();
    }
    //gets the data from the file needed via selecting the buttons name corresponding to the file in resources.
    public void LoadWriteButton()
    {
        LuaScript temp = player.luaInventory.GetScript(EventSystem.current.currentSelectedGameObject.name);
        player.luaInventory.currentScript = temp;
        LoadWriteButton(temp);
    }
    public void LoadWriteButton(LuaScript luaScript)
    {
        SaveInput.text = luaScript.scriptName;
        ScriptInput.text = luaScript.scriptText;
    }
    public void DeleteButton()
    {
        foreach(GameObject button in Buttons)
        {
            if (button.name == DeleteInput.text)
            {
                Buttons.Remove(button);
                Destroy(button);
                break;
            }
                
        }
        File.Delete(Path.path + DeleteInput.text + ".meta");
        File.Delete(Path.path + DeleteInput.text + Path.ext);
    }

    //This creates the buttons dynamically via an array, setting the button name to the txt file 
    public void ButtonMake()
    {
        if (SaveInput.text != "" && !System.Char.IsDigit(SaveInput.text[0]))
        {
            player.luaInventory.AddOrEdit(new LuaScript(SaveInput.text, ScriptInput.text));
            ButtonMake(SaveInput.text);
            overWrite();
        }
    
            
    }

    public void ButtonMake(string texts)
    {
        //Buttons = new GameObject[count];
        foreach (GameObject Button in Buttons)
        {
            if (Button.name == SaveInput.text)
                return;
        }
        
        GameObject temp = GameObject.Instantiate<GameObject>(Base, parent.transform);
        //temp.GetComponent<Button>().onClick.AddListener(deb);
        // temp.GetComponent<Button>().onClick.AddListener(delegate { deb("h"); });
        //temp.transform.SetParent(parent.transform);
        Buttons.Add(temp);

        temp.GetComponent<RectTransform>().localScale = new Vector3(1f, 1f, 1f);

        temp.name = texts;
        temp.GetComponentInChildren<Text>().text = texts;
        temp.SetActive(true);

    }
    void deb(string par)
    {
        Debug.Log(par);
    }
}

public struct Path
{
    public static string ext = ".txt";
    public static string path = "Assets/Resources/LuaFiles/";
}

