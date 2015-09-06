using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {

	public float speed = 40.0f;
	public GameObject owner;
	public GameObject target;
	private Rigidbody missileRigidbody;

	// Use this for initialization
	void Start () {
		missileRigidbody = GetComponent <Rigidbody> ();
		missileRigidbody.velocity = missileRigidbody.transform.forward * speed;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {
		if (target) {
			missileRigidbody.velocity = (target.transform.position - missileRigidbody.position).normalized * speed;
		}
//		missileRigidbody.AddForce ((target.transform.position - missileRigidbody.position).normalized * thrust);
	}

	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject != owner) {
			Destroy (this.gameObject);
		}
	}
}
