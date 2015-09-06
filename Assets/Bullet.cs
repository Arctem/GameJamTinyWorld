using UnityEngine;
using System;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float speed = 5.0f;
	public GameObject owner;
	private Rigidbody bulletRigidbody;
	private string[] listOfTargets = {"Gatherer", "Hunter", "ToyBox", "Revenge", "Cannon"};

	// Use this for initialization
	void Start () {
		bulletRigidbody = GetComponent <Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject != owner) {
			Destroy (this.gameObject);
		}
	}
}
