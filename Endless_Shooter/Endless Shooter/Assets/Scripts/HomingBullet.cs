using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    public float missileVelocity = 300f, turn = 20f, delay = 5f;
    private Rigidbody rb;
    private Transform target;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Fire();
        StartCoroutine(DestroyDelay());
    }

    private IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private void Fire()
    {
        float distance = Mathf.Infinity;
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            float diff = (g.transform.position - transform.position).sqrMagnitude;

            if(diff < distance)
            {
                distance = diff;
                target = g.transform;
            }
        }
    }

    private void FixedUpdate()
    {
        if (target == null || rb == null)
        {
            rb.velocity = transform.forward * missileVelocity;
        }
        else
        {
            rb.velocity = transform.forward * missileVelocity;

            Quaternion targetRotation = Quaternion.LookRotation(target.position - transform.position);
            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRotation, turn));
        }
    }
}
