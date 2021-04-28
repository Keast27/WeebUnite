using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{
    public static MenuScript instance;
    public bool pause = false;

    public GameObject pauseUI;

    public RawImage[] hearts;
    private int previousHealth = 6;

    public GameObject countUI;
    public Text countdown;
    public float countdownTime;
    private bool countdownDone = false;

    public GameObject player;
    public PlayerController playerScript;

    public bool inGame = true;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        countdownTime = 4f;
        player.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!inGame)
        {
            return;
        }

        //Handle Player Health
        if (playerScript.health != previousHealth)
        {
            HandleHealth();
        }


        if (Input.GetKeyDown(KeyCode.Escape) && pause == false)
        {
            pause = true;
            Time.timeScale = 0;
            pauseUI.SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pause == true)
        {
            pause = false;
            Time.timeScale = 1;
            pauseUI.SetActive(false);
        }

        countdownTime -= Time.deltaTime;

        if (countdownTime <= -1 && countdownDone == false)
        {
            countdownDone = true;
            countUI.SetActive(false);
            player.SetActive(true);
        }
        else if (countdownTime <= 0)
        {
            countdown.text = "Go!";
        }
        else if (countdownTime <= 1)
        {
            countdown.text = "1";
        }
        else if (countdownTime <= 2)
        {
            countdown.text = "2";
        }
        else if (countdownTime <= 3 )
        {
            countdown.text = "3";
        }
    }

    public void HandleHealth()
    {
        //If Player's Health is Full
        if (playerScript.health >= 6)
        {
            foreach (RawImage h in hearts)
            {
                h.gameObject.SetActive(true);
            }

            return;
        }

        //If Player's Health is Empty
        if (playerScript.health <= 0)
        {
            foreach (RawImage h in hearts)
            {
                h.gameObject.SetActive(false);
            }

            GameOver();

            return;
        }

        //Otherwise...
        for (int i = playerScript.health; i < hearts.Length; i++)
        {
            hearts[i].gameObject.SetActive(false);
        }

        for (int i = playerScript.health; i < 0; i--)
        {
            hearts[i].gameObject.SetActive(true);
        }

        previousHealth = playerScript.health;
    }

    public void Victory()
    {
        SceneManager.LoadScene("Victory");
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("Main");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void DoNotQuit()
    {
        pause = false;
        Time.timeScale = 1;
        pauseUI.SetActive(false);
    }

}
