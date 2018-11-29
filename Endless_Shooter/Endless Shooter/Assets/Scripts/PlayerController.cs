using System.Collections;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed, tilt;
    public Boundary boundary;
    public GameObject shield;

    public GameObject shot;
    public Transform[] shotSpawn;
    public float fireRate;

    private float nextFire;
    private int pattern = 0;
    private bool shieldUp = false;
    private int shields = 0;

    private GameController gc;

    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            switch(pattern)
            {
                case 3:
                    Instantiate(shot, shotSpawn[1].position, shotSpawn[1].rotation);
                    Instantiate(shot, shotSpawn[2].position, shotSpawn[2].rotation);
                    Instantiate(shot, shotSpawn[3].position, shotSpawn[1].rotation);
                    Instantiate(shot, shotSpawn[4].position, shotSpawn[2].rotation);
                    break;
                case 1:
                    Instantiate(shot, shotSpawn[1].position, shotSpawn[1].rotation);
                    Instantiate(shot, shotSpawn[2].position, shotSpawn[2].rotation);
                    break;
                case 2:
                    Instantiate(shot, shotSpawn[0].position, shotSpawn[1].rotation);
                    Instantiate(shot, shotSpawn[3].position, shotSpawn[2].rotation);
                    Instantiate(shot, shotSpawn[4].position, shotSpawn[2].rotation);
                    break;
                case 0:
                    Instantiate(shot, shotSpawn[0].position, shotSpawn[1].rotation);
                    break;
            }
            AudioSource audio = GetComponent<AudioSource>();
            audio.Play();
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

    public void PowerUp(int i)
    {
        switch (i)
        {
            case 0:
                Shield();
                break;
            case 1:
                SpeedUp();
                break;
            case 2:
                FirePattern();
                break;
            case 3:
                FireRate();
                break;
            case 4:
                ExtraLife();
                break;
        }
    }

    private void Shield()
    {
        if (!shieldUp)
        {
            SpawnShield();
            shieldUp = true;
        }
        shields++;
        if (shields > 5)
            shields = 5;
    }

    private void SpawnShield()
    {
        GameObject a = Instantiate(shield, transform.position, transform.rotation);
        a.transform.parent = gameObject.transform;
    }

    private void SpeedUp()
    {
        speed += 1;
        if (speed > 15)
            speed = 15;
    }

    private void FirePattern()
    {
        pattern++;
        if (pattern == 4)
            pattern = 3;
    }

    private void FireRate()
    {
        fireRate -= .03f;
        if (fireRate < .1f)
            fireRate = .1f;
    }

    private void ExtraLife()
    {
        GameObject gcObj = GameObject.FindWithTag("GameController");
        if (gcObj != null)
            gc = gcObj.GetComponent<GameController>();
        if (gc == null)
            Debug.Log("Cannot find 'GameContoller' script!");
        gc.addLive();
    }

    public void DestroyShield()
    {
        shields--;
        if (shields > 0)
            Invoke("SpawnShield", 2);
        else
            shieldUp = false;
    }
}
