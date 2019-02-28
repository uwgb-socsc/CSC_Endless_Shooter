using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed;
    public int MoveDirection = 0;

    void Start()
    {
        
        Rigidbody rb = GetComponent<Rigidbody>();
        switch(MoveDirection)
        {
            case 0:
                rb.velocity = transform.right * speed;
                break;
            case 1:
                rb.velocity = -(transform.right * speed);
                break;
            case 2:
                rb.velocity = transform.forward * speed;
                break;
            case 3:
                rb.velocity = -(transform.forward * speed);
                break;
            default:
                break;
        }
        
    }

}
