using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Must be used next to the Player Controller
/// </summary>
public class PlayerRaycast : MonoBehaviour {
    private Player player;
    public float rayDistance = 7;
    private Transform fpsCam;
    private RaycastHit hit;

    public GameObject interactableObjectCursor;
    public void Start()
    {
        player = GetComponent<Player>();
        fpsCam = player.fpsCam;
    }

    void Update()
    {
        //Continuously draws a debug ray and finds the hit
        Physics.Raycast(fpsCam.position, fpsCam.transform.forward, out hit, rayDistance);
        interactableObjectCursor.GetComponent<RectTransform>().position = fpsCam.GetComponent<Camera>().WorldToScreenPoint(fpsCam.position + fpsCam.transform.forward * rayDistance);
        interactableObjectCursor.GetComponent<RawImage>().color = Color.red;
        if (hit.collider)
        {
            
            if(hit.collider.GetComponent<ProgrammableObject>() && interactableObjectCursor != null)
            {
                interactableObjectCursor.GetComponent<RawImage>().color = Color.green;
            }
        }

        Debug.DrawRay(fpsCam.position, fpsCam.transform.forward * rayDistance, Color.red);
    }

    //Gets the first ProgrammableObject that it encounters
    public ProgrammableObject getFirstPO()
    {
        if (hit.collider != null)
            return hit.collider.GetComponent<ProgrammableObject>();
        else
            return null;
    }

    //Gets all of the ProgrammableObject that it encounters
    public List<ProgrammableObject> getAllPO()
    {
        RaycastHit[] hitAll = Physics.RaycastAll(fpsCam.position, fpsCam.transform.forward, rayDistance);

        List<ProgrammableObject> allPO = new List<ProgrammableObject>();
        foreach (RaycastHit _hit in hitAll)
        {
            ProgrammableObject PO;
            if((PO = _hit.collider.GetComponent<ProgrammableObject>()))
                allPO.Add(PO);
        }
        return allPO;
    }
}
