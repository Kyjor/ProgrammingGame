using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceScript {

    private static ForceScript _instance;
    public static ForceScript instance
    {
        get
        {
            if (_instance == null)
                _instance = new ForceScript();
            return _instance;
        }
        set { }
    }

	public IEnumerator AddF(float x, float y, float z, float f, ProgrammableObject PO)
    {
        
        PO.rb.AddForce(new Vector3(x, y, z) * f);

		if (PO.scriptQueue.Count > 0)
		{
			PO.fc.startCoroutine(PO.scriptQueue.Dequeue());
		}
		yield return null;
    }
	
}
