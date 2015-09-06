using UnityEngine;
using System.Collections;

public class pause_toggle : MonoBehaviour {

	public Canvas can;

	// Use this for initialization
	void Start () {
		can.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			can.enabled = !can.enabled;
			if (Time.timeScale == 1.0F)
				Time.timeScale = 0.0F;
			else
				Time.timeScale = 1.0F;
		}
	}
}
