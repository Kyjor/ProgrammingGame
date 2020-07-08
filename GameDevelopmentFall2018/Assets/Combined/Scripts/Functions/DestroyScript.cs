using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DestroyScript {

    private static DestroyScript _instance;
    public static DestroyScript instance
    {
        get
        {
            if (_instance == null)
                _instance = new DestroyScript();
            return _instance;
        }
        set { }
    }
    //   GameObject kill;
    //   public bool wrongObj;
    //   public bool cantFind;


    //// Update is called once per frame
    //void Update () {
    //       wrongObj = false;
    //       cantFind = false;
    //}

    //   public void Destroy(string obj)
    //   {

    //       Debug.Log("Destroy obj");
    //       if (obj == "gameObject")
    //           kill = gameObject;
    //       else
    //       {
    //           if (GameObject.Find(obj) != null)
    //           {
    //               kill = GameObject.Find(obj);
    //           }
    //           else
    //               cantFind = true;
    //       }

    //       if (kill.CompareTag("Clickable") && !cantFind)
    //       {

    //           Debug.Log("Tried to destroy");
    //           Destroy(kill);
    //           wrongObj = false;
    //           cantFind = false;
    //       }
    //       else if ((kill.CompareTag("Enemy") || kill.CompareTag("environemnt") || kill.CompareTag("player") || kill.CompareTag("robot")) && !cantFind)
    //       {
    //           wrongObj = true;
    //       }
    //   }

	public IEnumerator DestroyScriptableObject(int objID, ProgrammableObject PO)
    {
        ProgrammableObject targetPO = IDManager.getProgrammableObjectFromID(objID);
        if (targetPO != null)
        {
            UnityEngine.GameObject.Destroy(targetPO.gameObject);
            IDManager.removeObjectWithID(objID);


        }
		if (PO.scriptQueue.Count > 0)
		{
			PO.fc.startCoroutine(PO.scriptQueue.Dequeue());
		}
		yield return null;


    }
    }
