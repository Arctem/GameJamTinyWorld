using UnityEngine;
using System.Collections;

public class PauseScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp("space"))
        {
            if (Time.timeScale == 1.0f)
            {
                Time.timeScale = 0.0f;
            }
            else if (Time.timeScale == 0.0f)
            {
                Time.timeScale = 1.0f;
            }
        }
        if (Input.GetKeyUp("escape"))
            Application.Quit();
    }
}
