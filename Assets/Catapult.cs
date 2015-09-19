using UnityEngine;
using System.Collections;

public class Catapult : MonoBehaviour
{

    public float speed = 1f;
    public float turnSpeed = 30f;
    public GameObject owner;
    private Rigidbody catRigidbody;
    private GameObject target;
    public int health = 5;

    public float shootRange = 60f;
    public float timePerShot = 0.2f;
    public float shootCooldown = 2f;
    public int shotsPerClip = 5;
    public float bulletSpeed = 30f;
    public Bullet bulletPrefab;
    private float shootTimer = 0f;
    private float perShotTimer = 0f;
    private int shotsThisClip = 0;
    //Sounds
    public AudioClip SpawnSound;
    public AudioClip DeathSound;
    public AudioClip ActionSound;

    // Use this for initialization
    void Start()
    {
        catRigidbody = GetComponent<Rigidbody>();
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
        shootTimer -= Time.deltaTime;
        timePerShot -= Time.deltaTime;

        if (target)
        {
            Vector3 offset = transform.InverseTransformPoint(target.transform.position);
            if (offset.x < 0.0)
            {
                //target is to the left
                //this.transform.Rotate(Vector3.up * -turnSpeed);
                this.catRigidbody.AddRelativeTorque(0f, -turnSpeed, 0f);
            }
            else
            {
                //target is to the right
                //this.transform.Rotate(Vector3.up * turnSpeed);
                this.catRigidbody.AddRelativeTorque(0f, turnSpeed, 0f);
            }
            if (Mathf.Abs(offset.x) < 10.0 && offset.z > 0.0)
            {
                //If we're facing mostly towards it and it's in front of us.
                if (Vector3.Distance(catRigidbody.position, target.transform.position) < shootRange & shootTimer <= 0 & perShotTimer <= 0)
                {
                    Bullet clone = (Bullet)Instantiate(bulletPrefab,
                        catRigidbody.position + transform.forward * 2 + new Vector3(0f, 1f, 0f),
                        catRigidbody.rotation);
                    clone.GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
                    clone.owner = this.gameObject;
                    if (shotsThisClip < shotsPerClip)
                    {
                        shotsThisClip++;
                        perShotTimer = timePerShot;
                            }
                    else
                    {
                        shotsThisClip = 0;
                        shootTimer = shootCooldown;
                    }

                    GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioSource>().PlayOneShot(ActionSound, 1f);
                }
                else
                {
                    this.catRigidbody.AddRelativeForce(0f, 0f, speed);
                }
            }
        }
        else
        {
            GameObject[] possibles = GameObject.FindGameObjectsWithTag("ToyBox");
            foreach (GameObject test in possibles)
            {
                if (test != owner)
                {
                    target = test;
                }
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet" && collision.gameObject.GetComponent<Bullet>().owner != gameObject)
        {
            health--;
            owner.GetComponent<ToyBox>().DispatchRevenge(collision.gameObject.GetComponent<Bullet>().owner);
        }
        else if (collision.gameObject.tag == "Missile")
        {
            health--;
            owner.GetComponent<ToyBox>().DispatchRevenge(collision.gameObject.GetComponent<Missile>().owner);
        }
    }
}
