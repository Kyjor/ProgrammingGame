using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "Tools/POInfoTool")]
public class POInfoTool : BaseTool {
    public GameObject infoUIPrefab;
    private POInfoToolContainer container;
    //public InputField objectName;
    private ProgrammableObject objectPO;
    private GameObject UI;
    public POInfoTool()
    {
        //infoUI = null;
        toolName = "PO Info Tool";
    }

    public override void OnUseDown(Player player)
    {
        objectPO = player.pRay.getFirstPO();
        if (objectPO == null)
            return;
        player.Pause();

        //instantiate and enable infouiprefab
        if (UI == null)
        {
            UI = Instantiate<GameObject>(infoUIPrefab, player.canvas.transform);
            container = UI.GetComponent<POInfoToolContainer>();
        }
        else
            UI.SetActive(true);

        container.setup(player, objectPO);

        container.objectName.text = objectPO.objectName;
        container.ID.text = objectPO.id.ToString();
        container.scriptName.text = objectPO.luaScript.scriptName;
        container.WX.text = objectPO.gameObject.transform.position.x.ToString();
        container.WY.text = objectPO.gameObject.transform.position.y.ToString();
        container.WZ.text = objectPO.gameObject.transform.position.z.ToString();
        container.LX.text = objectPO.gameObject.transform.localPosition.x.ToString();
        container.LY.text = objectPO.gameObject.transform.localPosition.y.ToString();
        container.LZ.text = objectPO.gameObject.transform.localPosition.z.ToString();
    }

}
