using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowScript {

    private static FollowScript _instance;
    public static FollowScript instance
    {
        get
        {
            if (_instance == null)
                _instance = new FollowScript();
            return _instance;
        }
        set { }
    }
    //   GameObject targ;
    //   public bool cantFind;
    //   // Use this for initialization
    //   void Start () {
    //       cantFind = false;
    //}

    //// Update is called once per frame
    //void Update () {
    //       //transform.position = Vector3.Lerp(transform.position, new Vector3(335f, -11.899f, 301), Time.time);
    //   }

    //public void Follow(string x, float d, ProgrammableObject PO)
    //{

    //    PO.fc.startCoroutine(goTo(x, d));
    //}

    public void GoToObject(int objID, float duration, ProgrammableObject PO)
    {
        PO.fc.startCoroutine(GoToObj(objID, duration, PO));

    }

    //IEnumerator goTo(string x, float d)
    //{
    //    float t = 0f;
    //    if (GameObject.Find(x) != null) { 
    //        targ = GameObject.Find(x);
    //        Vector3 currPos = this.transform.position;
    //        while (t < 1)
    //        {

    //            t += Time.deltaTime / d;

    //            //transform.position = Vector3.MoveTowards(currPos, targ.transform.position, t); Pauses after each tap, requires multiple to move
    //            transform.position = Vector3.Lerp(this.transform.position, targ.transform.position, t); //Leads to instantaneous transportation
    //            cantFind = false;
    //            yield return null;
    //        }
    //    }
    //    else
    //    {
    //        cantFind = true;
    //    }
    //}

    public IEnumerator GoToObj(int objID, float duration,ProgrammableObject PO)
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
        float t = 0f;
        ProgrammableObject target = IDManager.getProgrammableObjectFromID(objID);
        if (target.gameObject != null)
        {
            Debug.Log(("Found"));
            Vector3 targ;
            
            while (t < duration)
            {
                targ = target.gameObject.transform.position;
                t += Time.deltaTime;
                PO.rb.WakeUp();
                yield return new WaitForEndOfFrame ();
                float lerpValue = Mathf.Clamp ( t / duration, 0,.9f);
                
                objToRun.transform.position = Vector3.Lerp(objToRun.transform.position, targ, lerpValue); //Leads to instantaneous transportation
             
            }
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
