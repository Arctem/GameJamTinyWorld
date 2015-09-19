using UnityEngine;
using System.Collections;

public class Gatherer : MonoBehaviour {

	public float speed = 2f;
	public float turnSpeed = 100f;
	public GameObject owner;
	private Rigidbody rb;
	private GameObject target;
	public int health = 3;
	//Sounds
	public AudioClip SpawnSound;
	public AudioClip DeathSound;
	public AudioClip ActionSound;

	// Use this for initialization
	void Start () {
		rb = GetComponent <Rigidbody>();
        GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>().PlayOneShot(SpawnSound, 1f);
    }
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
            GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>().PlayOneShot(DeathSound, 1f);
            Destroy (gameObject);
		}
	}

	void FixedUpdate () {
		if (target) {
			Vector3 offset = transform.InverseTransformPoint(target.transform.position);
			if(offset.x < 0.0) {
				//target is to the left
				//this.transform.Rotate(Vector3.up * -turnSpeed);
				this.rb.AddRelativeTorque(0f, -turnSpeed, 0f);
			} else {
				//target is to the right
				//this.transform.Rotate(Vector3.up * turnSpeed);
				this.rb.AddRelativeTorque(0f, turnSpeed, 0f);
			}
			if(Mathf.Abs(offset.x) < 10.0 && offset.z > 0.0) {
				//If we're facing mostly towards it and it's in front of us.
				this.rb.AddRelativeForce(0f, 0f, speed);
			}
		} else {
			GameObject[] possibles = GameObject.FindGameObjectsWithTag("Resource");
			GameObject currentBest = null;
			foreach(GameObject test in possibles) {
				if (currentBest) {
					if(Vector3.Distance(test.transform.position, rb.position) <
					   Vector3.Distance(currentBest.transform.position, rb.position)) {
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
				owner.GetComponent<ToyBox>().metal += 5;
				target = null;
			} else {
                GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>().PlayOneShot(ActionSound, 1f);
                target = owner;
			}
		} else if (collision.gameObject.tag == "Bullet") {
			health--;
			owner.GetComponent<ToyBox>().DispatchRevenge(collision.gameObject.GetComponent <Bullet>().owner);
		} else if (collision.gameObject.tag == "Missile") {
			health--;
			owner.GetComponent<ToyBox>().DispatchRevenge(collision.gameObject.GetComponent <Missile>().owner);
		}
	}
}
