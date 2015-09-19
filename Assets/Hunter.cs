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
	public int health = 2;
	private Rigidbody rb;
	private GameObject target;
	private float jumpTimer = 0f;
	private float shootTimer = 0f;
	//sound
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
		jumpTimer -= Time.deltaTime;
		shootTimer -= Time.deltaTime;

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
				if(Vector3.Distance(rb.position, target.transform.position) > shootRange) {
					if (jumpTimer < 0) {
						this.rb.AddRelativeForce(0f, jumpStrength, jumpStrength);
						jumpTimer = jumpCooldown;
					}
				} else if (target != owner && shootTimer < 0) {
                    GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>().PlayOneShot(ActionSound, 1f);
                    Bullet clone = (Bullet) Instantiate(bulletPrefab,
                  		rb.position + transform.forward * 2 + new Vector3(0f, 1f, 0f),
                        rb.rotation);
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
				if (test.GetComponent <Gatherer>()) {
					if (owner == test.GetComponent <Gatherer>().owner) {
						continue;
					}
				} else if(test.GetComponent<Catapult> () && owner == test.GetComponent <Catapult>().owner){
					continue;
				}
				if (currentBest) {
					if(Vector3.Distance(test.transform.position, rb.position) <
					   Vector3.Distance(currentBest.transform.position, rb.position)) {
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
			owner.GetComponent<ToyBox>().DispatchRevenge(collision.gameObject.GetComponent <Bullet>().owner);
		} else if (collision.gameObject.tag == "Missile") {
			health--;
			owner.GetComponent<ToyBox>().DispatchRevenge(collision.gameObject.GetComponent <Missile>().owner);
		}
	}
}
