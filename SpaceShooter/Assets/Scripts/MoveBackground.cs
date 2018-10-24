using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackground : MonoBehaviour {
    
    public float speed = 1f;

    void Update () {
        float step = speed * Time.deltaTime;
        transform.position -= new Vector3(0, 0, step);
        if(transform.position.z <= -15)
        {
            transform.position += new Vector3(0, 0, 30);
        }
    }
}
