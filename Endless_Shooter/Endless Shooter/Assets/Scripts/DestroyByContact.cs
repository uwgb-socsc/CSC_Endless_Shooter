using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public int damage = 1;
    private GameController gc;
    public GameObject bulExp, exp;
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
        if (!other.CompareTag("Enemy"))
        {
            if (other.CompareTag("ExploBullet"))
            {
                if (bulExp != null)
                {
                    Destroy(other.gameObject);
                    Instantiate(bulExp, transform.position, transform.rotation);
                    Destroy(gameObject);
                }
                else
                {
                    Destroy(other.gameObject);
                    Destroy(gameObject);
                }
            }
            else if(!other.CompareTag("Boundary"))
            {
                if (other.CompareTag("Player"))
                {
                    PlayerController pc = other.gameObject.GetComponent<PlayerController>();
                    if (pc == null)
                        Debug.Log("PC not found!");
                    pc.UpdateSlider(damage);
                }
                if (exp != null)
                {
                    Instantiate(exp, transform.position, transform.rotation);
                    Destroy(gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
