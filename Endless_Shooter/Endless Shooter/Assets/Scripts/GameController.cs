using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] normalHazards;
    public GameObject[] downHazards;
    public GameObject[] upHazards;
    public GameObject[] backHazards;
    public GameObject player;
    public Vector3 spawnValues;
    public Vector3 spawnValues1;
    public Vector3 spawnValues2;
    public Vector3 spawnValues3;
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

    void Start()
    {
        //gameOverText.text = "";
        //resetText.text = "";
        reset = false;
        gameOver = false;
        score = 0;
        //UpdateScore();
        //UpdateLives();
        StartCoroutine(SpawnWaves());
    }

    void Update()
    {
        if (reset)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = normalHazards[Random.Range(0, normalHazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                GameObject a = Instantiate(hazard, spawnPosition, spawnRotation);
                Mover m = a.GetComponent<Mover>();
                m.speed += -.05f * round;
                yield return new WaitForSeconds(spawnWait);
            }
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard1 = downHazards[Random.Range(0, downHazards.Length)];
                Vector3 spawnPosition1 = new Vector3(Random.Range(-spawnValues1.x, spawnValues.x), spawnValues1.y, spawnValues1.z);
                Quaternion spawnRotation = Quaternion.identity;
                GameObject b = Instantiate(hazard1, spawnPosition1, spawnRotation);
                Mover m = b.GetComponent<Mover>();
                m.speed += -.05f * round;
                yield return new WaitForSeconds(spawnWait);
            }
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard2 = upHazards[Random.Range(0, upHazards.Length)];
                Vector3 spawnPosition2 = new Vector3(Random.Range(-spawnValues2.x, spawnValues.x), spawnValues2.y, spawnValues2.z);
                Quaternion spawnRotation = Quaternion.identity;
                GameObject c = Instantiate(hazard2, spawnPosition2, spawnRotation);
                Mover m = c.GetComponent<Mover>();
                m.speed += -.05f * round;
                yield return new WaitForSeconds(spawnWait);
            }
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard3 = backHazards[Random.Range(0, backHazards.Length)];
                Vector3 spawnPosition3 = new Vector3(Random.Range(-spawnValues3.x, spawnValues.x), spawnValues3.y, spawnValues3.z);
                Quaternion spawnRotation = Quaternion.identity;
                GameObject d = Instantiate(hazard3, spawnPosition3, spawnRotation);
                Mover m = d.GetComponent<Mover>();
                m.speed += -.05f * round;
                yield return new WaitForSeconds(spawnWait);
            }
            round++;
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
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
        if (lives < 1)
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
