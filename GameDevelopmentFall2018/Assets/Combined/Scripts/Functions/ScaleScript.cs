using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
public class ScaleScript
{

    private static ScaleScript _instance;
    public static ScaleScript instance
    {
        get
        {
            if (_instance == null)
                _instance = new ScaleScript();
            return _instance;
        }
        set { }
    }

    public void Scale(float x, float y, float z, float duration, ProgrammableObject PO)
    {
        //attachedObjectScript.isScaling = true;
        PO.fc.startCoroutine(ScaleCoroutine(x, y, z, duration, PO));
        //coroutine = StartCoroutine();
    }

    public IEnumerator ScaleCoroutine(float x, float y, float z, float duration, ProgrammableObject PO)
    {
        GameObject objToRun = PO.gameObject;
        Vector3 init = objToRun.transform.localScale;
        Vector3 goal = new Vector3(x, y, z);
        float scaleDuration = 0;
        while (scaleDuration < duration)
        {
            float dTime = Time.deltaTime;
            scaleDuration += dTime;
            PO.rb.WakeUp();
            yield return new WaitForEndOfFrame();
            float lerpValue = Mathf.Clamp01(scaleDuration / duration);
            objToRun.transform.localScale = Vector3.Lerp(init, goal, lerpValue);
        }
        //attachedObjectScript.isScaling = false;*/
        if (PO.scriptQueue.Count > 0)
        {
			PO.fc.startCoroutine(PO.scriptQueue.Dequeue());
        }

        yield return 0;

    }

	public IEnumerator InstantScale(float x, float y, float z, ProgrammableObject PO)
    {
        PO.gameObject.transform.localScale = new Vector3(x, y, z);
        PO.rb.WakeUp();

		if (PO.scriptQueue.Count > 0)
		{
			PO.fc.startCoroutine(PO.scriptQueue.Dequeue());
		}
		yield return null;

    }




}
