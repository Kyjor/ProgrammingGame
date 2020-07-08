using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarTranslateScript : TranslateScript {

	public GameObject frontDriverWheel;
	public GameObject rearDriverWheel;
	public GameObject frontPassengerWheel;
	public GameObject rearPassengerWheel;
	public float speed = 500.0f;

	protected override void onTranslateAction(float dTime)
	{
		frontDriverWheel.GetComponent<Transform>().Rotate(Vector3.up * dTime * speed);
		rearDriverWheel.GetComponent<Transform>().Rotate(Vector3.up * dTime * speed);
		frontPassengerWheel.GetComponent<Transform>().Rotate(Vector3.up * dTime * speed);
		rearPassengerWheel.GetComponent<Transform>().Rotate(Vector3.up * dTime * speed);
	}
}
