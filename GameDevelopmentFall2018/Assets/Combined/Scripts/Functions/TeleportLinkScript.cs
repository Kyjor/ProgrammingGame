using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportLinkScript{

	private static TeleportLinkScript _instance;
    	public static TeleportLinkScript instance
   	 	{
        	get
        	{
            	if (_instance == null)
                _instance = new TeleportLinkScript();
				return _instance;
			}
			set { }
    }

	public IEnumerator CreateLink(int objID, ProgrammableObject PO)
	{
		ProgrammableObject target = IDManager.getProgrammableObjectFromID(objID);
		PO.teleportLink = target.gameObject.transform;
		if (PO.scriptQueue.Count > 0)
		{
			PO.fc.startCoroutine(PO.scriptQueue.Dequeue());
		}
		yield return null;
	}

	
}
