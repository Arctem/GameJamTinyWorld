using UnityEngine;
using System.Collections;
using System.Linq;

public class Hunter : MonoBehaviour {

	public float jumpStrength = 500f;
	public float jumpCooldown = 5f;
	public float turnSpeed = 100f;
	public float shootRange = 20f;
	public float shootCooldown = 3f;
	public float bulletSpeed = 30f;
	public GameObject owner;
	public Bullet bulletPrefab;
	public int health = 1;
	private Rigidbody rigidbody;
	private GameObject target;
	private float jumpTimer = 0f;
	private float shootTimer = 0f;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent <Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			//AudioSource.PlayClipAtPoint (DeathSound, rigidbody.transform.position);
			Destroy (gameObject);
		}
	}

	void FixedUpdate () {
		jumpTimer -= Time.deltaTime;
		shootTimer -= Time.deltaTime;

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
				if(Vector3.Distance(rigidbody.position, target.transform.position) > shootRange) {
					if (jumpTimer < 0) {
						this.rigidbody.AddRelativeForce(0f, jumpStrength, jumpStrength);
						jumpTimer = jumpCooldown;
					}
				} else if (target != owner && shootTimer < 0) {
					Bullet clone = (Bullet) Instantiate(bulletPrefab,
                  		rigidbody.position + transform.forward * 2 + new Vector3(0f, 1f, 0f),
                        rigidbody.rotation);
					clone.GetComponent <Rigidbody>().velocity = transform.forward * bulletSpeed;
					clone.owner = this.gameObject;
					shootTimer = shootCooldown;
				}
			}
		}
		if (!target || target == owner){
			GameObject[] possibles = GameObject.FindGameObjectsWithTag("Gatherer").Concat(GameObject.FindGameObjectsWithTag("Cannon")).ToArray();
			GameObject currentBest = null;
			foreach(GameObject test in possibles) {
				if (owner == (test.GetComponent <Gatherer>().owner)) {
					continue;
				}
				if (currentBest) {
					if(Vector3.Distance(test.transform.position, rigidbody.position) <
					   Vector3.Distance(currentBest.transform.position, rigidbody.position)) {
						currentBest = test;
					}
				} else {
					currentBest = test;
				}
			}
			target = currentBest ? currentBest : owner;
		}
	}

	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.tag == "Bullet" && collision.gameObject.GetComponent<Bullet>().owner != gameObject) {
			health--;
		}
	}
}
