using UnityEngine;
using System;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float speed = 5.0f;
	public GameObject owner;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs(gameObject.GetComponent<Rigidbody>().position.x) > 100 | Mathf.Abs(gameObject.GetComponent<Rigidbody>().position.z) > 100)
        {
            Destroy(this.gameObject);
        }
	}

	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject != owner) {
			Destroy (this.gameObject);
		}
	}
}
