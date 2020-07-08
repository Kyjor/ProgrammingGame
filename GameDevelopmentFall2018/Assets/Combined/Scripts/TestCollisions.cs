using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCollisions : MonoBehaviour {

	public GameObject prefab;
	public Transform spawnPoint;
	public Vector3 direction;
	public float speed;

	public void spawnObjectWithTag(string tag)
	{
		Vector3 start = spawnPoint.position;

		GameObject obj = (GameObject)Instantiate(prefab, start, Quaternion.identity);
		obj.gameObject.tag = tag;
		Vector3 velocity = direction * speed * 0.01f;
		obj.GetComponent<Rigidbody>().velocity = velocity;
	}
}
