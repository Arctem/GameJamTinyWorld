using UnityEngine;
using System.Collections;

public class ToyBox : MonoBehaviour {

	public float speed = 20f;
	public float turnSpeed = 10f;
	public string powerAxisName = "Vertical";
	public string turnAxisName = "Horizontal";
	public Gatherer gathererPrefab;
	public GameObject hunterPrefab;
	public GameObject revengePrefab;
	public GameObject cannonPrefab;
	public string gathererButton = "1";
	public string hunterButton = "2";
	public string revengeButton = "3";
	public string cannonButton = "4";
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
		if (Input.GetKeyDown (gathererButton)) {
			Gatherer clone = (Gatherer) Instantiate(gathererPrefab,
				carRigidbody.position + new Vector3(0f, 5f, 0f), carRigidbody.rotation);
			clone.owner = this.gameObject;
			clone.GetComponent <MeshRenderer>().material = this.GetComponent <MeshRenderer>().material;
		}
		if (Input.GetKeyDown (hunterButton)) {

		}
		if (Input.GetKeyDown (revengeButton)) {

		}
		if (Input.GetKeyDown (cannonButton)) {

		}
	}

	void FixedUpdate () {
		carRigidbody.AddRelativeForce(0f, 0f, powerInput * speed);
		carRigidbody.AddRelativeTorque(0f, turnInput * turnSpeed, 0f);
		if (Vector3.Angle (carRigidbody.transform.forward, carRigidbody.velocity) <= 90) {
			carRigidbody.velocity = carRigidbody.transform.forward * carRigidbody.velocity.magnitude;
		} else {
			carRigidbody.velocity = -carRigidbody.transform.forward * carRigidbody.velocity.magnitude;
		}
	}
}
