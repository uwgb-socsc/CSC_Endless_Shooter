using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    private GameController gc;
    public GameObject explo;
    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.FindWithTag("GameController");
        if (go != null)
            gc = go.GetComponent<GameController>();
        if (gc == null)
            Debug.Log("Could Not Find GameController!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController pc = other.gameObject.GetComponent<PlayerController>();
            pc.UpdateSlider(.1f);
            if (explo != null)
            {
                Instantiate(explo, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else if (other.CompareTag("Bullet"))
        {
            if (explo != null)
            {
                Destroy(other.gameObject);
                Instantiate(explo, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            else
            {
                Destroy(other.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
