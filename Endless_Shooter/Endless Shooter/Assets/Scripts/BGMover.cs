using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMover : MonoBehaviour {
    
    public float scrollSpeed;
    public float tileSizeX;

    private Vector3 startPosition;
    public GameController gc;

    void Start ()
    {
        startPosition = transform.position;
    }

	void Update () {
        if (!gc.pause)
        {
            float newPosition = Mathf.Repeat(Time.deltaTime * scrollSpeed, tileSizeX);
            transform.position = transform.position + Vector3.left * newPosition;
            if(transform.position.x < startPosition.x)
            {
                transform.position = new Vector3(transform.position.x + tileSizeX, transform.position.y, transform.position.z);
            }
        }
	}
}
