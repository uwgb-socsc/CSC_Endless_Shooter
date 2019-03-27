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
    public Slider slider;
    public GameObject explo;

    public float speed, tilt, health = 1f;
    public Boundary boundary;

    public GameObject shot;
    public Transform[] shotSpawn;
    public float fireRate;

    private float nextFire;
    private GameController gc;

    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn[0].position, shotSpawn[0].rotation);
            //AudioSource audio = GetComponent<AudioSource>();
            //audio.Play();
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Rigidbody rb = GetComponent<Rigidbody>();

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

    public void UpdateSlider(float remove)
    {
        health -= remove;
        if (health < 0)
            health = 0;
        slider.value = health;
        if (health == 0)
        {
            gc.GameOver();
            if (explo != null)
                Instantiate(explo, transform);
            Destroy(gameObject);
        }
    }
}
