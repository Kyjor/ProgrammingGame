using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class POInfoToolContainer : MonoBehaviour {

    public InputField objectName;
    public InputField ID;
    public InputField scriptName;
    public InputField WX, WY, WZ, LX, LY, LZ;
    private Player _player;
    private ProgrammableObject _objectPO;
    public void setup(Player player,ProgrammableObject objectPO)
    {
        _player = player;
        _objectPO = objectPO;
        objectName.onEndEdit.AddListener(delegate { objectPO.objectName = objectName.text; });
        ID.onEndEdit.AddListener(delegate { objectPO.id = int.Parse(ID.text); });

        WX.onEndEdit.AddListener(delegate {
            objectPO.transform.position =
            new Vector3(float.Parse(WX.text), objectPO.transform.position.y, objectPO.transform.position.z);
        });
        WY.onEndEdit.AddListener(delegate {
            objectPO.transform.position =
            new Vector3(objectPO.transform.position.x, float.Parse(WY.text), objectPO.transform.position.z);
        });
        WZ.onEndEdit.AddListener(delegate {
            objectPO.transform.position =
            new Vector3(objectPO.transform.position.x, objectPO.transform.position.y, float.Parse(WZ.text));
        });
        LX.onEndEdit.AddListener(delegate {
            objectPO.transform.localPosition =
            new Vector3(float.Parse(LX.text), objectPO.transform.localPosition.y, objectPO.transform.localPosition.z);
        });
        LY.onEndEdit.AddListener(delegate {
            objectPO.transform.localPosition =
            new Vector3(objectPO.transform.localPosition.x, float.Parse(LY.text), objectPO.transform.localPosition.z);
        });
        LZ.onEndEdit.AddListener(delegate {
            objectPO.transform.localPosition =
            new Vector3(objectPO.transform.localPosition.x, objectPO.transform.localPosition.y, float.Parse(LZ.text));
        });
        //objectName.onEndEdit.AddListener(delegate { objectPO.objectName = objectName.text; });
        //objectName.onEndEdit.AddListener(delegate { objectPO.objectName = objectName.text; });
    }
    public void closeUI()
    {
        this.gameObject.SetActive(false);
        _player.Resume();
    }

    public void Update()
    {
        if(_objectPO)
        {
            if(!WX.isFocused)
                WX.text = _objectPO.transform.position.x.ToString();
            if (!WY.isFocused)
                WY.text = _objectPO.transform.position.y.ToString();
            if (!WZ.isFocused)
                WZ.text = _objectPO.transform.position.z.ToString();
            if (!LX.isFocused)
                LX.text = _objectPO.transform.localPosition.x.ToString();
            if (!LY.isFocused)
                LY.text = _objectPO.transform.localPosition.y.ToString();
            if (!LZ.isFocused)
                LZ.text = _objectPO.transform.localPosition.z.ToString();
        }
    }
}
