using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class menu_actions : MonoBehaviour {

	public Text title;
	public Button play;
	public Button quit;
	public Button help;
	public Button ret;
	public Canvas help_m;
	public Canvas main;

	// Use this for initialization
	void Start () {
		title = title.GetComponent<Text>();
		play = play.GetComponent<Button>();
		quit = quit.GetComponent<Button>();	
		help = help.GetComponent<Button>();
		ret = ret.GetComponent<Button>();
		help_m = help_m.GetComponent<Canvas>();
		main = main.GetComponent<Canvas>();
		help_m.enabled = false;
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
	
	public void helpPress() {
		main.enabled = false;
		help_m.enabled = true;
	}
	
	public void returnPress() {
		help_m.enabled = false;
		main.enabled = true;
	}
}

