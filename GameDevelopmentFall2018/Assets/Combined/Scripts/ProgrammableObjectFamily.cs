using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgrammableObjectFamily : MonoBehaviour{
    public ProgrammableObject myParent = null;
    private ProgrammableObject itself;

    public Transform connectedPointTranform = null;
    public bool dynamicPoint = false;
    
    public List<ProgrammableObject> myChildren;
    public List<Transform> connectedPositionList = new List<Transform>();
    public void Start()
    {
        itself = GetComponent<ProgrammableObject>();
        myChildren = new List<ProgrammableObject>();
    }

    void setParentTransform(Transform parent)
    {
        this.transform.parent = parent;
    }
    void setParentObject(ProgrammableObject parent)
    {
        if (parent == null)
            myParent = null;
        else
            myParent = parent;
    }

    public void addChild(ProgrammableObject child , int connectingPoint = -1, bool ignoreCollision = true)
    {
        //if already connected
        if (myChildren.Contains(child))
        {
            Debug.Log("Child already connected to object.");
            return;
        }
        //if connecting point out of bounds
        if (connectedPositionList.Count <= connectingPoint)
        {
            Debug.Log("Connecting point is out of bounds.");
            return;
        }
        
        ProgrammableObjectFamily cFam = child.family;

        //Remove child from its own parent first before giving another parent
        if(cFam.myParent != null)
            cFam.myParent.family.removeChild(child);


        //if no connecting point given then add a point to list.
        if (connectingPoint == -1)
        {
            connectedPositionList.Add(new GameObject("Point").transform);
            cFam.connectedPointTranform = connectedPositionList[connectedPositionList.Count - 1];
            cFam.connectedPointTranform.parent = this.transform;
            cFam.connectedPointTranform.position = child.transform.position;
            cFam.dynamicPoint = true;
        }
        else
        {
            foreach (ProgrammableObject Ichild in myChildren)
            {
                if (Ichild.family.connectedPointTranform == connectedPositionList[connectingPoint])
                {
                    Debug.Log("that point already has an object connected to it.");
                    return;
                }
            }
            cFam.connectedPointTranform = connectedPositionList[connectingPoint];
        }
        //add child, set parent, give child connecting point, set transform of child to point, ignore collision
        myChildren.Add(child);

        cFam.setParentTransform(cFam.connectedPointTranform);
        cFam.setParentObject(itself);

        child.transform.position = cFam.connectedPointTranform.position;
        Physics.IgnoreCollision(this.GetComponent<Collider>(), child.GetComponent<Collider>(), ignoreCollision);

        Debug.Log("Added child " + child.name + " to " + this.name);
    }
    public void removeChild(int connectingPoint, bool isCollideParent = true)
    {
        if(connectingPoint >= connectedPositionList.Count)
        {
            Debug.Log("connected point in removeChild is out of bounds.");
            return;
        }

        if(connectedPositionList[connectingPoint] == null)
        {
            Debug.Log("That position does not have a point.");
            return;
        }

        foreach(ProgrammableObject child in myChildren)
        {
            if(child.family.connectedPointTranform == connectedPositionList[connectingPoint])
            {
                removeChild(child, isCollideParent);
                break;
            }
        }

        Debug.Log("No Children connected to that point");
    }
    public void removeChild(ProgrammableObject child, bool ignoreCollision = false)
    {
        if(!myChildren.Contains(child))
        {
            Debug.Log("Child is not from this parent.");
            return;
        }

        ProgrammableObjectFamily cFam = child.family;
        cFam.setParentObject(null);
        cFam.setParentTransform(null);
        myChildren.Remove(child);
        if(cFam.dynamicPoint == true)
        {
            connectedPositionList.Remove(cFam.connectedPointTranform);
            Destroy(cFam.connectedPointTranform.gameObject);
            cFam.dynamicPoint = false;
        }

        cFam.connectedPointTranform = null;
        Physics.IgnoreCollision(this.GetComponent<Collider>(), child.GetComponent<Collider>(), ignoreCollision);

        Debug.Log("Removed child " + child.name + " from " + this.name);

    }


}
