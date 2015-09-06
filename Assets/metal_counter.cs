using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class metal_counter : MonoBehaviour {

	private Text txt;
	public int metal;
	public GameObject owner;

	// Use this for initialization
	void Start () {
		txt = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		txt.text = metal.ToString();
		metal = (int) owner.GetComponent<ToyBox>().metal;
	}
}
