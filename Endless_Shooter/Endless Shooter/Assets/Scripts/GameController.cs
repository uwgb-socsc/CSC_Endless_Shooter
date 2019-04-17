using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public GameObject player;
    private PlayerController pc;

    public bool pause = false;
    public Slider liveSlider;
    public Text gameOverText;

    public int lives = 3;
    private bool reset = false;

    // Use this for initialization
    void Start () {

        Instantiate(player);
        pc = player.GetComponent<PlayerController>();
        liveSlider.value = lives;
        gameOverText.text = "";
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
        if(reset && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
	}

    public void Pause()
    {
        pause = !pause;
    }

    public void GameOver()
    {
        LifeLoss();
        if (lives > 0)
            Instantiate(player);
        else
        {
            gameOverText.text = "Game Over!\nPress 'R' to Reset";
            reset = true;
        }
    }

    private void LifeLoss()
    {
        lives--;
        liveSlider.value = lives;
    }
}
