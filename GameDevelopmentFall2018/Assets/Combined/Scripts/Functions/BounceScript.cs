using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceScript{

	private static BounceScript _instance;
	public static BounceScript instance
	{
		get
		{
			if (_instance == null)
				_instance = new BounceScript();
			return _instance;
		}
		set { }
	}
	public IEnumerator Bounce(int power, ProgrammableObject PO)
	{
		PO.bouncePwr = power;

		if (PO.scriptQueue.Count > 0)
		{
			PO.fc.startCoroutine(PO.scriptQueue.Dequeue());
		}
		yield return null;
	}
}
