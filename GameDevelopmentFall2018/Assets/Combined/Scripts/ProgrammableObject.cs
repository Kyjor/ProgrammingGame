using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgrammableObject : CollisionHandler
{

    public ProgrammableObjectFamily family;
    public Rigidbody rb;
    public string objectName = "empty";
    public LuaScript luaScript;
    public int id = 0;
    public FunctionCaller fc;

    public Player owner;
    public List<Player> coOwners;

    public float rbMass = 1;
    public float rbDrag = 1;

    public Transform teleportLink;
    public float bouncePwr;
    public ProgrammableObject activateLink;
    
    public bool isTranslating = false;
    public bool isOrbiting = false;
    public bool isRotating = false;
    public bool isScaling = false;
    
    public Queue<IEnumerator> scriptQueue;

    //Gets or creates a family and then sets a unique id.
    void Start()
    {
		scriptQueue = new Queue<IEnumerator> ();
        luaScript = new LuaScript();
        fc = new FunctionCaller();
        fc.setup(this, this);

        if ((family = GetComponent<ProgrammableObjectFamily>()) == null)
            family = gameObject.AddComponent<ProgrammableObjectFamily>();

        rb = GetComponent<Rigidbody>();
        if (!rb) gameObject.AddComponent<Rigidbody>();

        rbMass = rb.mass;
        rbDrag = rb.drag;
        IDManager.setID(this);
    }

    public void AddOwner(Player player)
    {
        owner = player;
    }
    public bool AddCoOwner(Player player)
    {
        if (coOwners.Contains(player))
            return false;

        coOwners.Add(player);
        return true;
    }
    public void RunScript()
    {
       // Queue<IEnumerator> scriptQueue = new Queue<IEnumerator>();
        fc.runScript();
    }
    public void RunAllChildrenScripts()
    {
        foreach (ProgrammableObject child in family.myChildren)
        {
            child.RunScript();
        }
    }
    //Written by: David Carlyn
    //Also modified the file to track the coroutine so we can stop it.
    protected override void onCollision(GameObject obj)
    {
        //isHit = true;
        //attachedObjectScript.isTranslating = false;
        //		Debug.Log("Collided with " + obj.name);
        fc.stopCoroutine();
    }

    //Written by: David Carlyn
    protected override void onRobotCollision(GameObject obj)
    {
        //isHit = true;
       // attachedObjectScript.isTranslating = false;
        Debug.Log("Hit Robot");
        fc.stopCoroutine();
        this.gameObject.GetComponent<Rigidbody>().useGravity = true;
    }

    //Written by: David Carlyn
    protected override void onEnvironmentCollision(GameObject obj)
    {
        //Literally do nothing
        Debug.Log("Environment Collision");
    }

    //Written by: David Carlyn
    protected override void onEnemyCollision(GameObject obj)
    {
        //isHit = true;
       // attachedObjectScript.isTranslating = false;
        Debug.Log("Enemy Collision");
        fc.stopCoroutine();

        Destroy(this.gameObject);
    }

    //Written by: David Carlyn
    protected override void onPlayerCollision(GameObject obj)
    {
        //Nothing
        Debug.Log("Player Collision");
        if (teleportLink != null)
        {
            obj.transform.position = teleportLink.transform.position;
        }

    }
}

