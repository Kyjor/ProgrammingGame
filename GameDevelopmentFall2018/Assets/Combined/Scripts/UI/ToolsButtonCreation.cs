using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ToolsButtonCreation : MonoBehaviour {
    public Player player;
    public GameObject toolButtonPrefab;
    public Transform toolsContainerTransform;
    public ToolsContainer InventoryTools;


	// Use this for initialization
	void Start () {
        CreateToolsButtons();
    }
	
    //Creates all the tools that the inventoryTools holds into the itemsInventoryUI.
    public void CreateToolsButtons()
    {
        for (int i = 0; i < InventoryTools.tools.Count; i++)
        {
            GameObject button = Instantiate<GameObject>(toolButtonPrefab,toolsContainerTransform);
            button.GetComponentInChildren<Text>().text = InventoryTools.tools[i].toolName;
            int j = i; // "By the time your closure is executed, your loop is over, 
                       //and i equals packet.listOfAnswers (that is true for all of your closures). Hence the argument out of range exception.
            button.GetComponent<Button>().onClick.AddListener(delegate { player.ChangeSwichableTool(InventoryTools.tools[j]); });
            button.name = InventoryTools.tools[j].toolName;
        }
    }

}
