using UnityEngine;
using System.Collections;

public class Gatherer : MonoBehaviour {

	public float speed = 2f;
	public float turnSpeed = 100f;
	public GameObject owner;
	private Rigidbody rigidbody;
	private GameObject target;
	public int health = 3;
	public bool dead = false; 
	//Sounds
	public AudioClip SpawnSound;
	public AudioClip DeathSound;
	public AudioClip ActionSound;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent <Rigidbody>();
		AudioSource.PlayClipAtPoint (SpawnSound, rigidbody.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0 && !dead) {
			AudioSource.PlayClipAtPoint (DeathSound, rigidbody.transform.position);
			dead = true; 
		}
	}

	void FixedUpdate () {
		if (target) {
			Vector3 offset = transform.InverseTransformPoint(target.transform.position);
			if(offset.x < 0.0) {
				//target is to the left
				//this.transform.Rotate(Vector3.up * -turnSpeed);
				this.rigidbody.AddRelativeTorque(0f, -turnSpeed, 0f);
			} else {
				//target is to the right
				//this.transform.Rotate(Vector3.up * turnSpeed);
				this.rigidbody.AddRelativeTorque(0f, turnSpeed, 0f);
			}
			if(Mathf.Abs(offset.x) < 10.0 && offset.z > 0.0) {
				//If we're facing mostly towards it and it's in front of us.
				this.rigidbody.AddRelativeForce(0f, 0f, speed);
			}
		} else {
			GameObject[] possibles = GameObject.FindGameObjectsWithTag("Resource");
			print (possibles);
			GameObject currentBest = null;
			foreach(GameObject test in possibles) {
				if (currentBest) {
					if(Vector3.Distance(test.transform.position, rigidbody.position) <
					   Vector3.Distance(currentBest.transform.position, rigidbody.position)) {
						currentBest = test;
					}
				} else {
					currentBest = test;
				}
			}
			target = currentBest;
		}
	}

	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject == target) {
			if (target == owner) {
				target = null;
			} else {
				AudioSource.PlayClipAtPoint(ActionSound, rigidbody.transform.position);
				target = owner;
			}
		}
	}
}
