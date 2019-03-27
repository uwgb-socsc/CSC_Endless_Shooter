using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private SphereCollider sCol;
    public float gRate = 2, expTime = .25f;
    private float elapsed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        sCol = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sCol != null)
        {
            sCol.radius += gRate * Time.deltaTime;
            elapsed += Time.deltaTime;
            if (expTime < elapsed)
            {
                Destroy(sCol);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player"))
        {
            Destroy(other.gameObject);
        }
    }
}
