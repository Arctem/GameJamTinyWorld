using UnityEngine;
using System.Collections;

public class Revenge : MonoBehaviour
{

    public float turnSpeed = 100f;
    public float shootRange = 40f;
    public float shootCooldown = 5f;
    public GameObject owner;
    public Missile missilePrefab;
    public int health = 2;
    public float minSpeed = 10.0f;
    public float maxSpeed = 50.0f;
    public float preferredAltitude = 30.0f;
    private Rigidbody planeRigidbody;
    public GameObject target = null;
    private float shootTimer = 0f;
    //Music
    public AudioClip SpawnSound;
    public AudioClip ActionSound;
    public AudioClip DeathSound;

    // Use this for initialization
    void Start()
    {
        planeRigidbody = GetComponent<Rigidbody>();
        planeRigidbody.velocity = planeRigidbody.transform.forward * minSpeed;
        preferredAltitude = preferredAltitude * Random.Range(0.5f, 1.5f);
        GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>().PlayOneShot(SpawnSound, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>().PlayOneShot(DeathSound, 1f);
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (planeRigidbody != null)
            if (target)
            {
                planeRigidbody.velocity = planeRigidbody.velocity + Time.deltaTime *
                    (target.transform.position - transform.position +
                     new Vector3(0.0f, preferredAltitude, 0.0f));
            }
            else
            {
                planeRigidbody.velocity = planeRigidbody.velocity + Time.deltaTime *
                    (owner.transform.position - transform.position +
                    new Vector3(0.0f, preferredAltitude, 0.0f));
            }

        EnforceSpeed();
        transform.LookAt(transform.position + planeRigidbody.velocity);

        shootTimer -= Time.deltaTime;
        if (target)
        {
            //print ((target.transform.position - planeRigidbody.position).magnitude);
            if ((target.transform.position - planeRigidbody.position).magnitude < shootRange && shootTimer < 0)
            {
                Missile clone = (Missile)Instantiate(missilePrefab,
                    planeRigidbody.position + transform.up * -2,
                    planeRigidbody.rotation);
                clone.owner = this.gameObject;
                clone.target = target;
                shootTimer = shootCooldown;
                target = null;
                GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>().PlayOneShot(ActionSound, 1f);
            }
        }
    }

    void EnforceSpeed()
    {
        if (planeRigidbody.velocity.magnitude < minSpeed)
        {
            planeRigidbody.velocity = planeRigidbody.velocity.normalized * minSpeed;
        }
        else if (planeRigidbody.velocity.magnitude > maxSpeed)
        {
            planeRigidbody.velocity = planeRigidbody.velocity.normalized * maxSpeed;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            health--;
            owner.GetComponent<ToyBox>().DispatchRevenge(collision.gameObject.GetComponent<Bullet>().owner);
        }
        else if (collision.gameObject.tag == "Missile" && collision.gameObject.GetComponent<Missile>().owner != gameObject)
        {
            health--;
            owner.GetComponent<ToyBox>().DispatchRevenge(collision.gameObject.GetComponent<Missile>().owner);
        }
    }
}
