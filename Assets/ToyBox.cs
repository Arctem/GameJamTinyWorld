using UnityEngine;
using System.Collections;

public class ToyBox : MonoBehaviour {

	public float speed = 20f;
	public float turnSpeed = 10f;
	public string powerAxisName = "Vertical";
	public string turnAxisName = "Horizontal";
	private float powerInput;
	private float turnInput;
	private Rigidbody carRigidbody;

	// Use this for initialization
	void Start () {
		carRigidbody = GetComponent <Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		powerInput = Input.GetAxis (powerAxisName);
		turnInput = Input.GetAxis (turnAxisName);
	}

	void FixedUpdate () {
		carRigidbody.AddRelativeForce(0f, 0f, powerInput * speed);
		carRigidbody.AddRelativeTorque(0f, turnInput * turnSpeed, 0f);
		carRigidbody.velocity = carRigidbody.transform.forward * carRigidbody.velocity.magnitude;
	}
}
