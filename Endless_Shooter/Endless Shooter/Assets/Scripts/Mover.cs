using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour {

    public float speed;
    private GameController gc;
    Rigidbody rb;
    private bool jc = false;

    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        if (gc == null)
            Debug.Log("GC not found!");
        rb = gameObject.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    private void FixedUpdate()
    {
        if(gc.pause)
        {
            rb.velocity = Vector3.zero;
            jc = true;
        }
        else if(jc)
        {
            rb.velocity = transform.forward * speed;
            jc = false;
        }
    }
}
