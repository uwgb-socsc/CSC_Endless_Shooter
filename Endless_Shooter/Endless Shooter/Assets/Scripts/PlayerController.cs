using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    private Slider healthSlider;
    public GameObject explo;

    public float speed, tilt;
    public int health = 10;
    public Boundary boundary;

    public GameObject shot;
    public Transform[] shotSpawn;
    public float fireRate;
    
    private float nextFire;
    private GameController gc;

    private Rigidbody rb;

    void Start()
    {
        healthSlider = GameObject.Find("Health").GetComponent<Slider>();
        health = 10;
        healthSlider.value = health;
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!gc.pause)
        {
            if (Input.GetButton("Fire1") && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;
                Instantiate(shot, shotSpawn[0].position, shotSpawn[0].rotation);
                //AudioSource audio = GetComponent<AudioSource>();
                //audio.Play();
            }
        }
    }

    void FixedUpdate()
    {
        if (!gc.pause)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            rb.velocity = movement * speed;

            rb.position = new Vector3
            (
                Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
            );

            rb.rotation = Quaternion.Euler(rb.velocity.z * tilt, 0.0f, 0.0f);
        }
        else
        {
            rb.velocity = Vector3.zero;
        }
    }

    public void UpdateSlider(int remove)
    {
        health -= remove;
        if (health < 0)
            health = 0;
        healthSlider.value = health;
        if (health == 0)
        {
            gc.GameOver();
            if (explo != null)
                Instantiate(explo, transform);
            Destroy(gameObject);
        }
    }
}
