using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour {

    public static GameObject CreateObject(Player player,GameObject prefab, float SpawnDistance = 5)
    {
        RaycastHit hit;
        Vector3 pos = player.fpsCam.transform.position + player.fpsCam.transform.forward * SpawnDistance;
        Physics.Raycast(player.fpsCam.position, player.fpsCam.transform.forward, out hit, SpawnDistance);

        if (hit.collider != null)
            return null;
        else
        {
            return Instantiate<GameObject>(prefab, pos, Quaternion.identity);
        }
    }
         
}
