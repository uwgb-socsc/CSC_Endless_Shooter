using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject[] powerUps;
    
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    public int powerUp;

    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if(gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!CompareTag("PowerUp"))
        {
            if (other.CompareTag("Boundary") || other.CompareTag("Enemy") || other.CompareTag("PowerUp"))
            {
                return;
            }

            if (explosion != null)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }

            if (other.CompareTag("Player"))
            {
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                gameController.GameOver();
            }
            else if (other.CompareTag("Shield"))
            {
                GameObject player = GameObject.FindWithTag("Player");
                PlayerController pc = player.GetComponent<PlayerController>();
                pc.DestroyShield();
            }
            else if (powerUps.Length >= 1)
            {
                switch (Random.Range(0, 100))
                {
                    case 0:
                    case 1:
                    case 2:
                        Instantiate(powerUps[0], transform.position, Quaternion.Euler(0.0f, 0.0f, 0.0f));
                        break;
                    case 3:
                    case 4:
                        Instantiate(powerUps[1], transform.position, Quaternion.Euler(0.0f, 0.0f, 0.0f));
                        break;
                    case 5:
                    case 6:
                        Instantiate(powerUps[2], transform.position, Quaternion.Euler(0.0f, 0.0f, 0.0f));
                        break;
                    case 7:
                    case 8:
                        Instantiate(powerUps[3], transform.position, Quaternion.Euler(0.0f, 0.0f, 0.0f));
                        break;
                    case 9:
                        Instantiate(powerUps[4], transform.position, Quaternion.Euler(0.0f, 0.0f, 0.0f));
                        break;
                }
            }
            gameController.AddScore(scoreValue);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
        else
        {
            if(other.CompareTag("Player"))
            {
                PlayerController pc = other.GetComponent<PlayerController>();
                if(pc == null)
                {
                    Debug.Log("Cannot find 'PlayerController' script!");
                }
                pc.PowerUp(powerUp);
                gameController.AddScore(scoreValue);
                Destroy(gameObject);
            }
        }
    }
}
