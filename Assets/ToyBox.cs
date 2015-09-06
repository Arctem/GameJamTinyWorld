using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ToyBox : MonoBehaviour {

	public float speed = 20f;
	public float turnSpeed = 10f;
	public string powerAxisName = "Vertical";
	public string turnAxisName = "Horizontal";
	public Gatherer gathererPrefab;
	public Hunter hunterPrefab;
	public Revenge revengePrefab;
	public Catapult cannonPrefab;
	public string gathererButton = "1";
	public string hunterButton = "2";
	public string revengeButton = "3";
	public string cannonButton = "4";
	public int health = 100;
	public List<Revenge> revenges = new List<Revenge>();
	private float powerInput;
	private float turnInput;
	private Rigidbody carRigidbody;
	public float metal = 20f;
	private int ReadyForAnotherShwoo = 0;

	//Sounds
	public AudioClip MoveSound;
	public AudioClip DeathSound;

	// Use this for initialization
	void Start () {
		carRigidbody = GetComponent <Rigidbody>();
		print (MoveSound);
	}
	
	// Update is called once per frame
	void Update () {
		metal += Time.deltaTime;

		powerInput = Input.GetAxis (powerAxisName);
		turnInput = Input.GetAxis (turnAxisName);
		if (Input.GetKeyDown (gathererButton) && metal > 10) {
			metal -= 10;
			Gatherer clone = (Gatherer) Instantiate(gathererPrefab,
				carRigidbody.position + new Vector3(0f, 5f, 0f), carRigidbody.rotation);
			clone.owner = this.gameObject;
			clone.GetComponent <MeshRenderer>().material = this.GetComponent <MeshRenderer>().material;
		}
		if (Input.GetKeyDown (hunterButton) && metal > 30) {
			metal -= 30;
			Hunter clone = (Hunter) Instantiate(hunterPrefab,
				carRigidbody.position + new Vector3(0f, 5f, 0f), carRigidbody.rotation);
			clone.owner = this.gameObject;
			clone.GetComponentInChildren <MeshRenderer>().material = this.GetComponent <MeshRenderer>().material;
		}
		if (Input.GetKeyDown (revengeButton) && metal > 50) {
			metal -= 50;
			Revenge clone = (Revenge) Instantiate(revengePrefab,
      			carRigidbody.position + new Vector3(0f, 20f, 0f), carRigidbody.rotation);
			clone.owner = this.gameObject;
			clone.GetComponent <MeshRenderer>().material = this.GetComponent <MeshRenderer>().material;
			revenges.Add(clone);
		}
		if (Input.GetKeyDown (cannonButton) && metal > 70) {
			metal -= 70;
			Catapult clone = (Catapult) Instantiate(cannonPrefab,
            	carRigidbody.position + new Vector3(0f, 5f, 0f), carRigidbody.rotation);
			clone.owner = this.gameObject;
			clone.GetComponentInChildren <MeshRenderer>().material = this.GetComponent <MeshRenderer>().material;
		}
	}

	void FixedUpdate () {
		if (powerInput > 0 && ReadyForAnotherShwoo == 0) {
			AudioSource.PlayClipAtPoint(MoveSound, carRigidbody.transform.position);
			ReadyForAnotherShwoo += 100;
		}
		if (ReadyForAnotherShwoo > 0) {
			ReadyForAnotherShwoo--;
		}
		if (health == 0) {
			AudioSource.PlayClipAtPoint (DeathSound, carRigidbody.transform.position);
			Destroy (gameObject);
		}
		carRigidbody.AddRelativeForce(0f, 0f, powerInput * speed);
		carRigidbody.AddRelativeTorque(0f, turnInput * turnSpeed, 0f);
		if (Vector3.Angle (carRigidbody.transform.forward, carRigidbody.velocity) <= 90) {
			carRigidbody.velocity = carRigidbody.transform.forward * carRigidbody.velocity.magnitude;
		} else {
			carRigidbody.velocity = -carRigidbody.transform.forward * carRigidbody.velocity.magnitude;
		}
	}

	//Check list of Revengebots and send one after aggressor if there's a free one.
	public void DispatchRevenge (GameObject aggressor) {
		foreach (Revenge rev in revenges) {
			if(!rev.target || rev.target == gameObject) {
				rev.target = aggressor;
				return;
			}
		}
	}

	void OnCollisionEnter (Collision collision) {
		if (collision.gameObject.tag == "Bullet") {
			health--;
			DispatchRevenge(collision.gameObject.GetComponent <Bullet>().owner);
		}
	}
}
