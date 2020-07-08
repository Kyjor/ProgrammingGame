using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireProjectileScript{

	private static FireProjectileScript _instance;
    public static FireProjectileScript instance
    {
        get
        {
            if (_instance == null)
                _instance = new FireProjectileScript();
            return _instance;
        }
        set { }
    }
	
	public IEnumerator FireProjectile(float direction, float speed, float size, ProgrammableObject PO)
    {
        
        GameObject projectile =MonoBehaviour.Instantiate(MonoBehaviour.FindObjectOfType<PrefabManager>().projectilePrefab, PO.gameObject.transform.position, Quaternion.identity, null) as GameObject;
        projectile.GetComponent<BoxCollider>().isTrigger = true;
        projectile.transform.localScale = new Vector3(size, size, size);
        Vector3 test = PO.gameObject.transform.forward * direction;
        //float test2 = test.normalized;
        Quaternion rotation = Quaternion.Euler(0, direction, 0);
        var modifiedDirection = rotation * PO.gameObject.transform.forward;
        projectile.GetComponent<Rigidbody>().AddForce(modifiedDirection.normalized * speed, ForceMode.VelocityChange);

		if (PO.scriptQueue.Count > 0)
		{
			PO.fc.startCoroutine(PO.scriptQueue.Dequeue());
		}
		yield return null;

    }
}
