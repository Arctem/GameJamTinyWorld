using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class menu_actions : MonoBehaviour {

	public Button play;
	public Button quit;

	// Use this for initialization
	void Start () {
		play = play.GetComponent<Button>();
		quit = quit.GetComponent<Button>();	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void playPress() {
		Application.LoadLevel(1);
	}

	public void quitPress() {
		Application.Quit();
	}
}

