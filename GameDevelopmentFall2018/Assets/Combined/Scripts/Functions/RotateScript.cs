using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RotateScript{
    
	private static RotateScript _instance;
    public static RotateScript instance
    {
        get
        {
            if (_instance == null)
                _instance = new RotateScript();
            return _instance;
        }
        set { }
    }
    // I don't have much experience with what this does, but I assume its needed in a similare manor due to the 
    //fact both are moving the object
	public void Rotate(string axis, float rotateDegrees, float duration, ProgrammableObject PO)
	{
		PO.fc.startCoroutine(RotateCoroutine (axis,rotateDegrees, duration, PO));
	}
    
     public IEnumerator RotateCoroutine(string axis, float rotateDegrees, float duration, ProgrammableObject PO)
    {
		GameObject objToRun = PO.gameObject;
        objToRun.GetComponent<Rigidbody>().isKinematic = true;
        float rotateTime = 0;
        
		float init = objToRun.transform.rotation.y;
        float goal = objToRun.transform.rotation.y + rotateDegrees;

        if (axis == "x" || axis == "X")
        {
            while (rotateTime < duration)
            {
                float dTime = Time.deltaTime;
                rotateTime += dTime;
                yield return new WaitForEndOfFrame();
                float lerpValue = Mathf.Clamp01(rotateTime / duration);
                objToRun.transform.rotation = Quaternion.Euler(Mathf.Lerp(init, goal, lerpValue), objToRun.transform.rotation.y, objToRun.transform.rotation.z);
                PO.rb.WakeUp();

            }

            //rotate the
        }
        else if (axis == "y" || axis == "Y")
        {
            

            while (rotateTime < duration)
            {
                float dTime = Time.deltaTime;
                rotateTime += dTime;
                yield return new WaitForEndOfFrame();
                float lerpValue = Mathf.Clamp01(rotateTime / duration);
                objToRun.transform.rotation = Quaternion.Euler(objToRun.transform.rotation.x, Mathf.Lerp(init, goal, lerpValue), objToRun.transform.rotation.z);
                PO.rb.WakeUp();
            }
        }

        else if (axis == "z" || axis == "Z")
        {

            while (rotateTime < duration)
            {
                float dTime = Time.deltaTime;
                rotateTime += dTime;
                yield return new WaitForEndOfFrame();
                float lerpValue = Mathf.Clamp01(rotateTime / duration);
                objToRun.transform.rotation = Quaternion.Euler(objToRun.transform.rotation.x,  objToRun.transform.rotation.y, Mathf.Lerp(init, goal, lerpValue));
                PO.rb.WakeUp();

            }
        }

        else
            //The user didn't input x y or z
            yield return null;
        objToRun.GetComponent<Rigidbody>().isKinematic = false;

		if (PO.scriptQueue.Count > 0)
		{
			PO.fc.startCoroutine(PO.scriptQueue.Dequeue());
		}
		yield return null;
    }
   
}
