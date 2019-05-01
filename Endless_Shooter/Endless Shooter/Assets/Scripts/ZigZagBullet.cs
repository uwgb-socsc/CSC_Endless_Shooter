using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZigZagBullet : MonoBehaviour
{
    public float speed;
    private GameController gc;
    Rigidbody rb;
    private bool goUp = true;
    Vector3 up, down;
    float switchDelay, curDelay;

    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (gc == null)
            Debug.Log("GC not found!");
        rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
        up = new Vector3(.5f, 0, 1f) * speed;
        down = new Vector3(.5f, 0, -1f) * speed;
        if (Random.Range(0, 2) == 0)
        {
            rb.velocity = up;
            goUp = true;
        }
        else
        {
            rb.velocity = down;
            goUp = false;
        }
        if (speed != 0)
            switchDelay = 1f / speed;
        else
            switchDelay = 1f;
        curDelay = switchDelay / 2;
    }
    
    void FixedUpdate()
    {
        if (gc.pause)
        {
            rb.velocity = Vector3.zero;
        }
        else
        {
            curDelay += Time.deltaTime;
            if(curDelay >= switchDelay)
            {
                if (goUp)
                    rb.velocity = down;
                else
                    rb.velocity = up;
                goUp = !goUp;
                curDelay = 0f;
            }
        }
    }
}
