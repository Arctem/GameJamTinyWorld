using UnityEngine;
using System.Collections;

public class Revenge : MonoBehaviour {
	
	public float turnSpeed = 100f;
	public float shootRange = 20f;
	public float shootCooldown = 5f;
	public GameObject owner;
//	public Missile missilePrefab;
	public int health = 3;
	public float minSpeed = 10.0f;
	public float maxSpeed = 50.0f;
	public float preferredAltitude = 30.0f;
	private Rigidbody planeRigidbody;
	private GameObject target;
	private float jumpTimer = 0f;
	private float shootTimer = 0f;

	// Use this for initialization
	void Start () {
		planeRigidbody = GetComponent <Rigidbody> ();
		planeRigidbody.velocity = planeRigidbody.transform.forward * minSpeed;
		preferredAltitude = preferredAltitude * Random.Range (0.5f, 1.5f);
	}
	
	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate () {
		if (target) {
		} else {
			planeRigidbody.velocity = planeRigidbody.velocity + Time.deltaTime *
				(owner.transform.position - transform.position +
				new Vector3(0.0f, preferredAltitude, 0.0f));
		}

		EnforceSpeed ();
		transform.LookAt (transform.position + planeRigidbody.velocity);
	}

	void EnforceSpeed() {
		if (planeRigidbody.velocity.magnitude < minSpeed) {
			planeRigidbody.velocity = planeRigidbody.velocity.normalized * minSpeed;
		} else if (planeRigidbody.velocity.magnitude > maxSpeed) {
			planeRigidbody.velocity = planeRigidbody.velocity.normalized * maxSpeed;
		}
	}
}
