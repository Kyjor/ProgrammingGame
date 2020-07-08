using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestVelocity : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = this.transform.position;
		pos.x += 0.01f;

		this.gameObject.transform.position = pos;
	}
}
