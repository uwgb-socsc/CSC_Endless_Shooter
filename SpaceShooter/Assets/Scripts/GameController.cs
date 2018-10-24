using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public GameObject player;
    public Vector3 spawnValues;
    public int hazardCount;
    public float respawnWait;
    public float spawnWait;
    public float startWait;
    public float waveWait;
    public float speedInc;
    public int lives;

    public Text scoreText;
    public Text resetText;
    public Text gameOverText;
    public Text livesText;

    private bool reset;
    private bool gameOver;
    private int score;
    private int round = 0;

    void Start ()
    {
        gameOverText.text = "";
        resetText.text = "";
        reset = false;
        gameOver = false;
        score = 0;
        UpdateScore();
        UpdateLives();
        StartCoroutine (SpawnWaves());
	}

    void Update()
    {
        if(reset)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
	
	IEnumerator SpawnWaves ()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0,hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                GameObject a = Instantiate(hazard, spawnPosition, spawnRotation);
                Mover m = a.GetComponent<Mover>();
                m.speed += -.05f * round;
                yield return new WaitForSeconds(spawnWait);
            }
            round++;
            yield return new WaitForSeconds(waveWait);

            if(gameOver)
            {
                if (lives < 1)
                {
                    resetText.text = "Press 'R' to Restart";
                    reset = true;
                    break;
                }
                else
                {
                    StartCoroutine(Timer());
                    break;
                }
            }
        }
    }

    IEnumerator Timer()
    {
        int time = (int)respawnWait * 100;
        while (time > 0)
        {
            gameOverText.text = "Respawn in:\n" + ((float)time / 100f).ToString("0.00");
            yield return new WaitForSeconds(.01f);
            time -= 1;
            if (time < 0)
                time = 0;
        }
        Instantiate(player, transform.position, transform.rotation);
        gameOverText.text = "";
        gameOver = false;
        StartCoroutine(SpawnWaves());
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        lives--;
        UpdateLives();
        if(lives < 1)
            gameOverText.text = "Game Over!";
        gameOver = true;
    }

    void UpdateLives()
    {
        livesText.text = "Lives: " + lives;
    }

    public void addLive()
    {
        lives++;
        if (lives > 5)
            lives = 5;
        UpdateLives();
    }
}
