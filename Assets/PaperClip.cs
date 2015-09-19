using UnityEngine;
using System.Collections;

public class PaperClip : MonoBehaviour {

    public int randSeed;

	// Use this for initialization
	void Start () {
        System.Random randTime = new System.Random(System.DateTime.Now.GetHashCode());
        System.Random randTrajectory = new System.Random(randSeed);
        gameObject.GetComponent<Rigidbody>().velocity = new Vector3(((float)(randTrajectory.NextDouble() - 0.5) + (float)((randTime.NextDouble() - 0.5) * 5)) * 5.0f, -50f, ((float)(randTrajectory.NextDouble() - 0.5) + (float)((randTime.NextDouble() - 0.5) * 5)) * 5.0f);
	}
	
	// Update is called once per physics update
	void FixedUpdate () {
        //Make sure the paper clip isn't stuck in the ground
        if (gameObject.GetComponent<Rigidbody>().transform.position.y < -0.2f)
            gameObject.GetComponent<Rigidbody>().position = new Vector3(gameObject.GetComponent<Rigidbody>().position.x, 0.2f, gameObject.GetComponent<Rigidbody>().position.z);
    }
}
