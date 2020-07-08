using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class EditorMenu : MonoBehaviour
{
    //The menu object
    public GameObject UIMenu;
    public GameObject inventoryMenu;
    public ButtonScript buttonScript;
    //This enables the menu to become active when you click on editor window

    //Enables editor window menu
    public void OpenEditorMenu()
    {
        UIMenu.SetActive(true);


    }
    public void OpenInventoryMenu()
    {
        inventoryMenu.SetActive(true);
    }
}
