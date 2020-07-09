using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public string playerName;
    public int playerID;

    [HideInInspector]
    public PlayerMovement pMove;
    [HideInInspector]
    public PlayerRaycast pRay;
    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public Rigidbody rbody;

    public GameObject canvas;
    [HideInInspector]
    public BaseTool currentTool;
    private BaseTool switchableTool;
    int currentToolIndex;
    public ToolsContainer ToolsInventoryStart;
    public LuaInventory luaInventory = new LuaInventory();
    public Transform fpsCam;
    public EditorMenu UIMenu;
    public bool Menu = false;

    public List<ProgrammableObject> ownedPO = new List<ProgrammableObject>();
    public List<ProgrammableObject> sharedPO = new List<ProgrammableObject>();
    public GameObject hotBarContainer;
    List<GameObject> hotBarContainerElements = new List<GameObject>();
    public GameObject HotbarItemPrefab;
    public GameObject hotBarIndicatorPrefab;
    GameObject hotBarIndicator;
    

    public void Awake()
    {
        pMove = GetComponent<PlayerMovement>();
        pRay = GetComponent<PlayerRaycast>();
        anim = GetComponent<Animator>();
        rbody = GetComponent<Rigidbody>();
        SetShowCursor(false);

        currentToolIndex = 0;
        for (int i = 0; i < ToolsInventoryStart.tools.Count; i++)
        {
            hotBarContainerElements.Add(Instantiate<GameObject>(HotbarItemPrefab, hotBarContainer.transform));
            hotBarContainerElements[i].GetComponentInChildren<Text>().text = ToolsInventoryStart.tools[i].toolName;
        }

        hotBarIndicator = Instantiate<GameObject>(hotBarIndicatorPrefab, hotBarContainerElements[0].transform);
        currentTool = ToolsInventoryStart.tools[0];
        currentTool.OnSwitchIn(this);
        switchableTool = ToolsInventoryStart.tools[ToolsInventoryStart.tools.Count - 1];
    }

    public void Update()
    {

        if (Menu)
            return;

        currentTool.ToolUpdate(this);
        getInput();

    }
    private void FixedUpdate()
    {
        pMove.Move();
        if (Menu)
            return;

        
        currentTool.ToolFixedUpdate(this);
        fireInputFixed();
    }

    
    private void getInput()
    {
        scrollWheelInputHotBar();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Pause();
            UIMenu.OpenInventoryMenu();
            return;
        }
        if (Input.GetButton("Use"))
        {

            if(pRay.getFirstPO() != null)
            {
                if (pRay.getFirstPO().owner == this)
                {
                    Pause();
                    UIMenu.OpenEditorMenu();
                    UIMenu.buttonScript.LoadWriteButton(pRay.getFirstPO().luaScript);
                    return;
                }
            }
            else
            {
                Pause();
                UIMenu.OpenEditorMenu();
                return;
            }
        }

        fireInput();


    }
    private void fireInput()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            currentTool.OnUseDown(this);
        }
        else if (UnityEngine.Input.GetButton("Fire1"))
        {
            currentTool.OnUseHold(this);
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            currentTool.OnUseUp(this);
        }
    }
    private void fireInputFixed()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            currentTool.OnUseDownFixed(this);
        }
        else if (UnityEngine.Input.GetButton("Fire1"))
        {
            currentTool.OnUseHoldFixed(this);
        }
        else if (Input.GetButtonUp("Fire1"))
        {
            currentTool.OnUseUpFixed(this);
        }
    }
    private void scrollWheelInputHotBar()
    {
        var scrollDirection = UnityEngine.Input.GetAxis("Mouse ScrollWheel");
        print("HI");
        if (scrollDirection > 0)
        {
            //up
            if (ToolsInventoryStart.tools.Count - 2 > currentToolIndex)
            {
                currentToolIndex++;
                SwitchTool(ToolsInventoryStart.tools[currentToolIndex]);
            }
            else if (ToolsInventoryStart.tools.Count - 2 == currentToolIndex)
            {
                currentToolIndex++;
                SwitchTool(switchableTool);
            }
        }
        else if (scrollDirection < 0)
        {
            //down
            if (currentToolIndex > 0)
            {
                currentToolIndex--;
                SwitchTool(ToolsInventoryStart.tools[currentToolIndex]);
            }
        }
    }

    public void Pause()
    {
        Menu = true;
        anim.SetFloat("inputH", 0);
        anim.SetFloat("inputV", 0);
        SetShowCursor(true);
    }

    public void Resume()
    {
        Menu = false;
        SetShowCursor(false);
    }
    public void SetShowCursor(bool showCursor)
    {
        if (showCursor == false)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
        Cursor.visible = showCursor;
    }
    public void PlayerSpawnObject(GameObject prefab)
    {
        GameObject newObj = SpawnObject.CreateObject(this, prefab);
        if(newObj != null)
        {
            ProgrammableObject newObjPO = newObj.GetComponent<ProgrammableObject>();
            ownedPO.Add(newObjPO);
            newObjPO.AddOwner(this);
        }
    }

    public bool removeOwnedObject(ProgrammableObject ownedObjPO)
    {
        return ownedPO.Remove(ownedObjPO);
    }
    
    //Switches current tool to another tool. Used in Scrollwheel and ocassionally in the inventory buttons when the places is over the last tool.
    public void SwitchTool(BaseTool toolToSwitch)
    {
        currentTool.OnSwitchOut(this);
        currentTool = toolToSwitch;
        currentTool.OnSwitchIn(this);
        Destroy(hotBarIndicator);
        hotBarIndicator = Instantiate<GameObject>(hotBarIndicatorPrefab, hotBarContainerElements[currentToolIndex].transform);
    }
    //Changes the switchable tool which is always the last one in the items Hotbar.
    public void ChangeSwichableTool(BaseTool toolToSwitch)
    {
        switchableTool = toolToSwitch;
        //Changes UI name of the last tool in inventory.
        hotBarContainerElements[hotBarContainerElements.Count - 1].GetComponentInChildren<Text>().text = toolToSwitch.toolName;

        //if player has last tool in current then switch the current tool to new tool.
        if (currentToolIndex == ToolsInventoryStart.tools.Count - 1)
            SwitchTool(switchableTool);
    }
}