using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class TranslateScript
{
	private static TranslateScript _instance;
    public static TranslateScript instance
    {
        get
        {
            if (_instance == null)
                _instance = new TranslateScript();
            return _instance;
        }
        set { }
    }


	public Dictionary<string, List<Vector3>> listDictionary = new Dictionary<string, List<Vector3>>(); //Added By Kyle Conel


	//Added By Kyle Conel
	public IEnumerator AddTranslateVector(string listName, float x, float y, float z, ProgrammableObject PO)
	{
		if(!listDictionary.ContainsKey(listName))// If listName key is not in the dictionary, create it.
		{
			listDictionary.Add (listName, new List<Vector3> ());
		}
		//Otherwise, add this vector to the list in the dictionary
		listDictionary [listName].Add (new Vector3 (x, y, z));

		if (PO.scriptQueue.Count > 0)
		{
			PO.fc.startCoroutine(PO.scriptQueue.Dequeue());
		}
		yield return null;
	}

    //Added By Kyle Conel: 
	public void Translate(string listName, float duration, int loopCount, ProgrammableObject PO)
    {
		//attachedObjectScript.isTranslating = true;
        PO.fc.startCoroutine(TranslateCoroutine(listName, duration, loopCount, PO));
    }

    public void Move(string listName, float duration, int loopCount, ProgrammableObject PO)
    {
        //attachedObjectScript.isTranslating = true;
        PO.fc.startCoroutine(MoveCoroutine(listName, duration, loopCount, PO));
    }

    public IEnumerator TranslateCoroutine(string listName, float duration, int loopCount, ProgrammableObject PO)
    {
		GameObject objToRun = PO.gameObject;
		if (objToRun.GetComponent<Rigidbody> () == null) 
		{
			objToRun.AddComponent<Rigidbody> ();
		}
		//duration: The amount of time taken to move between each point
		
		//loopCount: The amount of times the player travels to all of the points. if the number is below 0, it is an infinite loop. 0 will not work
		objToRun.GetComponent<Rigidbody>().isKinematic = true;
		objToRun.GetComponent<Rigidbody> ().useGravity = false;
		while (loopCount > 0 || loopCount < 0) 
		{
			foreach (Vector3 point in listDictionary[listName]) 
			{
				float translateTime = 0;
				Vector3 init = objToRun.transform.position;
				Vector3 goal = point;
				float translateDuration = duration;
				while (translateTime < duration) 
				{
		//			isTranslating = true;
					float dTime = Time.deltaTime;
					translateTime += dTime;
					translateDuration -= dTime;
					PO.rb.WakeUp();
					yield return new WaitForEndOfFrame ();
					float lerpValue = Mathf.Clamp01 (translateTime / duration);
					objToRun.transform.position = Vector3.Lerp (init, goal, lerpValue);
				}
			}
			if (loopCount > 0) 
			{
				--loopCount;
			}
		}
		objToRun.GetComponent<Rigidbody>().isKinematic = false;
        objToRun.GetComponent<Rigidbody>().useGravity = true;
		//isTranslating = false;
		////attachedObjectScript.isTranslating = false;
		if (PO.scriptQueue.Count > 0)
		{
			PO.fc.startCoroutine(PO.scriptQueue.Dequeue());
		}
		yield return null;
	}

    public IEnumerator MoveCoroutine(string listName, float duration, int loopCount, ProgrammableObject PO)
    {
        GameObject objToRun = PO.gameObject;
        if (objToRun.GetComponent<Rigidbody>() == null)
        {
            objToRun.AddComponent<Rigidbody>();
        }
        //duration: The amount of time taken to move between each point

        //loopCount: The amount of times the player travels to all of the points. if the number is below 0, it is an infinite loop. 0 will not work
        objToRun.GetComponent<Rigidbody>().isKinematic = true;
        objToRun.GetComponent<Rigidbody>().useGravity = false;
        while (loopCount > 0 || loopCount < 0)
        {
            foreach (Vector3 point in listDictionary[listName])
            {
                float translateTime = 0;
                Vector3 init = objToRun.transform.position;
                Vector3 goal = init + point;
                float translateDuration = duration;
                while (translateTime < duration)
                {
                    //			isTranslating = true;
                    float dTime = Time.deltaTime;
                    translateTime += dTime;
                    translateDuration -= dTime;
	                PO.rb.WakeUp();
                    yield return new WaitForEndOfFrame();
                    float lerpValue = Mathf.Clamp01(translateTime / duration);
                    objToRun.transform.position = Vector3.Lerp(init, goal, lerpValue);
                }
            }
            if (loopCount > 0)
            {
                --loopCount;
            }
        }
        objToRun.GetComponent<Rigidbody>().isKinematic = false;
        objToRun.GetComponent<Rigidbody>().useGravity = true;
        //isTranslating = false;
        ////attachedObjectScript.isTranslating = false;
		if (PO.scriptQueue.Count > 0)
		{
			PO.fc.startCoroutine(PO.scriptQueue.Dequeue());
		}
		yield return null;
    }

    //private void stopCoroutine()
    //{
    //	if (coroutine == null)
    //	{

    //		return;
    //	}
    //	attachedObjectScript.isTranslating = false;

    //	StopCoroutine(coroutine);
    //}
    protected virtual void onTranslateAction(float dTime)
    {

    }
}