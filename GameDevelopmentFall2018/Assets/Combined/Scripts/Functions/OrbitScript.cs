using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitScript{
	private static OrbitScript _instance;
    public static OrbitScript instance
    {
        get
        {
            if (_instance == null)
                _instance = new OrbitScript();
            return _instance;
        }
        set { }
    }
	
	

	public void Orbit(int x, int y, int z, float radius, float speed, float duration, ProgrammableObject PO)
	{
		PO.fc.startCoroutine(OrbitCoroutine (x, y, z, radius, speed, duration, PO));

	}
	public IEnumerator OrbitCoroutine(int x, int y, int z, float radius, float speed, float duration, ProgrammableObject PO)
	{
		GameObject objToRun = PO.gameObject;
        objToRun.GetComponent<Rigidbody>().isKinematic = true;
        objToRun.GetComponent<Rigidbody>().useGravity = false;
        //Setting the variables of this instance to the values input by the user
        Vector3 desiredPosition; //Next position we are moving towards in orbit
		float stepLength = 0.015f;
		Vector3 centerOfOrbit = new Vector3 (x, y, z);
		float orbitRadius = radius;
		float orbitSpeed = speed;
		
		//Initializing the positioning of the item
		objToRun.transform.position = (objToRun.transform.position - centerOfOrbit).normalized * radius + centerOfOrbit;

		while (duration > 0) 
		{
			duration -= stepLength;
			objToRun.transform.RotateAround (centerOfOrbit, Vector3.up, stepLength * orbitSpeed);
			desiredPosition = (objToRun.transform.position - centerOfOrbit).normalized * orbitRadius + centerOfOrbit;
			objToRun.transform.position = Vector3.MoveTowards (objToRun.transform.position, desiredPosition, stepLength);
            PO.rb.WakeUp();
			yield return new WaitForSecondsRealtime (stepLength);
		}
        objToRun.GetComponent<Rigidbody>().isKinematic = false;
        objToRun.GetComponent<Rigidbody>().useGravity = true;

		if (PO.scriptQueue.Count > 0)
		{
			PO.fc.startCoroutine(PO.scriptQueue.Dequeue());
		}
		yield return null;

	}

}
