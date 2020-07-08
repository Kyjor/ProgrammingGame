using UnityEngine;
using System.Collections;
using System;

[CreateAssetMenu(menuName = "Tools/Physics Gun Tool")]
class PhysicsGunTool : BaseTool
{
    private ProgrammableObject objectPO;
    private Rigidbody objectBody;
    private Rigidbody pointBody;
    private SpringJoint pointString;
    private Transform playerCamPosition;

    private Vector3 destinationVector;
    private Vector3 destinationPoint;

    private float playerToPoint;
    [Tooltip("controls the strength of the spring")]
    public float springStrength = 1000;
    [Tooltip("controls the speed that it follows the forward point.")]
    public float springDamper = 100;
    [Tooltip("controls the oscilation strength")]
    public float objectDragSet = 100;

    private bool freezeObject = false;

    public PhysicsGunTool()
    {
        toolName = "Physics Gun";
    }

    public override void OnSwitchIn(Player player)
    {
        if(!playerCamPosition)
            playerCamPosition = player.fpsCam;

        if (pointBody == null)
        {
            GameObject point = new GameObject("PhysicsToolPoint");
            point.layer = 2;
            pointBody = point.AddComponent<Rigidbody>();
            point.AddComponent<BoxCollider>();
            point.GetComponent<Collider>().isTrigger = true;
            pointBody.isKinematic = true;

            pointString = point.gameObject.AddComponent<SpringJoint>();
            pointString.autoConfigureConnectedAnchor = false;
            pointString.anchor = Vector3.zero;
            pointString.connectedAnchor = Vector3.zero;
            pointString.spring = springStrength;
            pointString.damper = springDamper;
        }
    }
    public override void OnSwitchOut(Player player)
    {
        OnUseUp(player);
    }

    public override void ToolUpdate(Player player)
    {
        if (pointBody)
        {
            destinationVector = playerCamPosition.forward * playerToPoint;
            destinationPoint = playerCamPosition.position + destinationVector;
            pointBody.transform.position = destinationPoint;
        }         
    }

    public override void OnUseDown(Player player)
    {
        objectPO = player.pRay.getFirstPO();
        if (objectPO == null)
            return;

        //set rigidbody info
        objectBody = objectPO.gameObject.GetComponent<Rigidbody>();
        objectBody.drag = objectDragSet;
        objectBody.useGravity = false;
        objectBody.freezeRotation = true;
        objectBody.isKinematic = false;

        // Distance should between positions should be calculated.
        playerToPoint = Vector3.Distance(playerCamPosition.position, objectPO.transform.position);

        pointString.connectedBody = objectBody;
        OnUseHold(player);
    }
    public override void OnUseHold(Player player)
    {
        if (objectPO == null || freezeObject)
            return;

        

        if (Input.GetButtonDown("Fire2"))
        {
            freezeObject = true;
            pointString.connectedBody = null;
            objectBody.isKinematic = true;
            objectPO = null;
        }

    }
    public override void OnUseUp(Player player)
    {
        if (freezeObject)
            freezeObject = false;
        if (objectPO == null)
            return;

        pointString.connectedBody = null;

        objectBody.drag = objectPO.rbDrag;
        objectBody.useGravity = true;
        objectBody.freezeRotation = false;
        objectBody.isKinematic = false;
    }

    

}