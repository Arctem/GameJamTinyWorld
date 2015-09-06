using UnityEngine;
using System.Collections;

public class Catapult : MonoBehaviour {

	public float speed = 1f;
	public float turnSpeed = 30f;
	public GameObject owner;
	private Rigidbody catRigidbody;
	private GameObject target;
	public int health = 3;

	public float shootRange = 60f;
	public float shootCooldown = 0.5f;
	public float bulletSpeed = 30f;
	public Bullet bulletPrefab;
	private float shootTimer = 0f;
	//Sounds
	public AudioClip SpawnSound;
	public AudioClip DeathSound;
	public AudioClip ActionSound;

	// Use this for initialization
	void Start () {
		catRigidbody = GetComponent <Rigidbody>();
//		AudioSource.PlayClipAtPoint (SpawnSound, catRigidbody.transform.position);
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
//			AudioSource.PlayClipAtPoint (DeathSound, catRigidbody.transform.position);
			Destroy (gameObject);
		}
	}

	void FixedUpdate() {
		shootTimer -= Time.deltaTime;

		if (target) {
			Vector3 offset = transform.InverseTransformPoint(target.transform.position);
			if(offset.x < 0.0) {
				//target is to the left
				//this.transform.Rotate(Vector3.up * -turnSpeed);
				this.catRigidbody.AddRelativeTorque(0f, -turnSpeed, 0f);
			} else {
				//target is to the right
				//this.transform.Rotate(Vector3.up * turnSpeed);
				this.catRigidbody.AddRelativeTorque(0f, turnSpeed, 0f);
			}
			if(Mathf.Abs(offset.x) < 10.0 && offset.z > 0.0) {
				//If we're facing mostly towards it and it's in front of us.
				if(Vector3.Distance(catRigidbody.position, target.transform.position) < shootRange) {
//					AudioSource.PlayClipAtPoint (ActionSound , catRigidbody.transform.position);
					Bullet clone = (Bullet) Instantiate(bulletPrefab,
                        catRigidbody.position + transform.forward * 2 + new Vector3(0f, 1f, 0f),
                        catRigidbody.rotation);
					clone.GetComponent <Rigidbody>().velocity = transform.forward * bulletSpeed;
					clone.owner = this.gameObject;
					shootTimer = shootCooldown;
				} else {
					this.catRigidbody.AddRelativeForce(0f, 0f, speed);
				}
			}
		} else {
			GameObject[] possibles = GameObject.FindGameObjectsWithTag("ToyBox");
			foreach(GameObject test in possibles) {
				if(test != owner) {
					target = test;
				}
			}
		}
	}

	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.tag == "Bullet" && collision.gameObject.GetComponent<Bullet>().owner != gameObject) {
			health--;
			owner.GetComponent<ToyBox>().DispatchRevenge(collision.gameObject.GetComponent <Bullet>().owner);
		} else if (collision.gameObject.tag == "Missile") {
			health--;
			owner.GetComponent<ToyBox>().DispatchRevenge(collision.gameObject.GetComponent <Missile>().owner);
		}
	}
}
