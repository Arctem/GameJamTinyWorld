using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class health_bar : MonoBehaviour {

	public RawImage img;
	public int playerHP;
	public int hp_change;
	public GameObject owner;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		playerHP = owner.GetComponent<ToyBox> ().health;
		if (playerHP <= hp_change) {
			img = gameObject.GetComponentInChildren<RawImage>();
			Destroy(img);
			hp_change -= 10;
		}
	}
}
