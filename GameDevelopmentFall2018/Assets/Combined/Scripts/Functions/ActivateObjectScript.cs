using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateObjectScipt{

	private static ActivateObjectScipt _instance;
    	public static ActivateObjectScipt instance
   	 	{
        	get
        	{
            	if (_instance == null)
                _instance = new ActivateObjectScipt();
				return _instance;
			}
			set { }
    }

	public void Activate(int objID, ProgrammableObject PO)
	{
		ProgrammableObject target = IDManager.getProgrammableObjectFromID(objID);
		PO.activateLink = target;
	}

	
}
