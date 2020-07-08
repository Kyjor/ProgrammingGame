using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter;
using UnityEngine.UI;
using System;
using System.IO;
using UnityEditor;
using System.Timers;
using System.Collections.Generic;


public class FunctionCaller
{

    public delegate void OrderedFunctions();

    public OrderedFunctions ToCall;
    public delegate void Action<T1, T2, T3, T4, T5>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5);
    public delegate void Action<T1, T2, T3, T4, T5, T6>(T1 p1, T2 p2, T3 p3, T4 p4, T5 p5, T6 p6);
    public GameObject programObject;

    Script LuaRunner;
    private DynValue luaCo;
    public MonoBehaviour mono;
    public ProgrammableObject thisPO;
    public UnityEngine.Coroutine myCoroutine;
    
        private static Timer aTimer;
    
        public static void Main()
        {
            // Create a timer and set a two second interval.
            aTimer = new System.Timers.Timer();
            aTimer.Interval = 1;
    
            // Hook up the Elapsed event for the timer. 
            //aTimer.Elapsed += OnTimedEvent;
    
            // Have the timer fire repeated events (true is the default)
            aTimer.AutoReset = true;
    
            // Start the timer
            aTimer.Enabled = true;
    
            
        }
    
//        private static void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
//        {
//            Debug.Log("The Elapsed event was raised at {0}", e.SignalTime);
//        }

    public void setup(MonoBehaviour mo, ProgrammableObject PO)
    {
        mono = mo;
        thisPO = PO;
        LuaRunner = new Script();
        PlaceScriptsOnObject(LuaRunner);
    }

    public void runScript()
    {
        String code = wrapInLuaCoroutine(thisPO.luaScript.scriptText);
        //DynValue function =  LuaRunner.DoString(code);
        //luaCo = LuaRunner.CreateCoroutine((function));
		LuaRunner.DoString(code);
		if (thisPO.scriptQueue.Count > 0) 
		{
			startCoroutine (thisPO.scriptQueue.Dequeue ());
		}
    }

    public void startCoroutine(IEnumerator method)
    {
        myCoroutine =  mono.StartCoroutine(method);
    }

    public void stopCoroutine()
    {
        if (myCoroutine != null)
        {
            mono.StopCoroutine(myCoroutine);

        }
        mono.GetComponent<Rigidbody>().isKinematic = false;
        mono.GetComponent<Rigidbody>().useGravity = true;
    }


    public void PlaceScriptsOnObject(Script sc)
    {
        //UserData.RegisterType<AttachedObjectScript>();
       //DynValue obj = UserData.Create(thisPO);

        Action<float, float, float, float> Scale = (float x, float y, float z, float time) =>
        {
            /*ScaleScript.instance.Scale(x, y, z, time, thisPO);*/
            IEnumerator temp = ScaleScript.instance.ScaleCoroutine(x, y, z, time, thisPO); 
			thisPO.scriptQueue.Enqueue(temp);};
       sc.Globals["Scale"] = Scale;


        Action<float, float, float> InstantScale = (float x, float y, float z) => { //ScaleScript.instance.InstantScale(x, y, z,thisPO); 
			IEnumerator temp = ScaleScript.instance.InstantScale(x, y, z, thisPO); 
			thisPO.scriptQueue.Enqueue(temp);};
		
        sc.Globals["InstantScale"] = InstantScale;


        Action<string, float, int> Translate = (string listName, float duration, int loopCount) => { //TranslateScript.instance.Translate(listName, duration, loopCount, thisPO); 
			IEnumerator temp = TranslateScript.instance.TranslateCoroutine(listName,duration,loopCount, thisPO); 
			thisPO.scriptQueue.Enqueue(temp);};
        sc.Globals["Trans"] = Translate;


        Action<string, float, float, float> AddVector = (string listName, float x, float y, float z) => { //TranslateScript.instance.AddTranslateVector(listName, x, y, z); 
			IEnumerator temp = TranslateScript.instance.AddTranslateVector(listName,x, y, z, thisPO); 
			thisPO.scriptQueue.Enqueue(temp);};
        sc.Globals["AddV"] = AddVector;


        Action<string, float, float> Rotate = (string axis, float rotateDegrees, float duration) => { //RotateScript.instance.Rotate(axis, rotateDegrees, duration, thisPO); 
			IEnumerator temp = RotateScript.instance.RotateCoroutine(axis,rotateDegrees,duration, thisPO); 
			thisPO.scriptQueue.Enqueue(temp);};
        sc.Globals["Rotate"] = Rotate;


        Action<float, float, float> FireProjectile = (float direction, float speed, float size) => { //FireProjectileScript.instance.FireProjectile(direction, speed, size, thisPO); 
			IEnumerator temp = FireProjectileScript.instance.FireProjectile(direction,speed,	size, thisPO); 
			thisPO.scriptQueue.Enqueue(temp);};
        sc.Globals["Shoot"] = FireProjectile;


        Action<int, int, int, float, float, float> Orbit = (int x, int y, int z, float radius, float speed, float duration) => { //OrbitScript.instance.Orbit(x, y, z, radius, speed, duration, thisPO); 
			IEnumerator temp = OrbitScript.instance.OrbitCoroutine(x, y, z, radius,speed,duration, thisPO); 
			thisPO.scriptQueue.Enqueue(temp);};
        sc.Globals["Orbit"] = Orbit;


        Action<int> Destroy = (int objID) => { //DestroyScript.instance.DestroyScriptableObject(objID); 
			IEnumerator temp = DestroyScript.instance.DestroyScriptableObject(objID, thisPO); 
			thisPO.scriptQueue.Enqueue(temp);};
        sc.Globals["Destroy"] = Destroy;


        Action<int,float> GoTo = (int objID, float duration) => { //FollowScript.instance.GoToObject(objID, duration, thisPO); 
			IEnumerator temp = FollowScript.instance.GoToObj(objID,duration, thisPO); 
			thisPO.scriptQueue.Enqueue(temp);};
        sc.Globals["GoTo"] = GoTo;
        

        Action<float, float, float, float> AddForce = (float x, float y, float z, float f) => { //ForceScript.instance.AddF(x, y, z, f, thisPO); 
			IEnumerator temp = ForceScript.instance.AddF(x, y, z, f, thisPO); 
			thisPO.scriptQueue.Enqueue(temp);};
        sc.Globals["AddForce"] = AddForce;
        

        Action<float, float, float, float> Color = (float r, float g, float b, float a) =>
        {
            /*ColorScript.instance.ChangeColor(r, g, b, a, thisPO);*/
            IEnumerator temp = ColorScript.instance.ChangeColor(r, g, b, a, thisPO); 
			thisPO.scriptQueue.Enqueue(temp);};
        sc.Globals["Color"] = Color;


        Action<string, float, int> Move = (string listName, float duration, int loopCount) => { //TranslateScript.instance.Move(listName, duration, loopCount, thisPO); 
			IEnumerator temp = TranslateScript.instance.MoveCoroutine(listName,duration,loopCount, thisPO); 
			thisPO.scriptQueue.Enqueue(temp);};
        sc.Globals["Move"] = Move;
        

        Action<int> CreateLink = (int objID) => { //TeleportLinkScript.instance.CreateLink(objID, thisPO); 
			IEnumerator temp = TeleportLinkScript.instance.CreateLink(objID, thisPO); 
			thisPO.scriptQueue.Enqueue(temp);};
        sc.Globals["CreateLink"] = CreateLink;
        

        Action<int> Bounce = (int power) => { BounceScript.instance.Bounce(power, thisPO); 
		};
        sc.Globals["Bounce"] = Bounce;
        
        Action<int> Activate = (int objID) => { ActivateObjectScipt.instance.Activate(objID, thisPO); };
        sc.Globals["Activate"] = Activate;
        
        //sc.Globals.Set ("obj", obj);
    }
    private String wrapInLuaCoroutine(String luaCode)
    {
        String result = "co = coroutine.create(function() \n"
            + luaCode + "\n"
            + "end)" + "\n"
            + "print(coroutine.resume(co))";
        return result;
    }
}

