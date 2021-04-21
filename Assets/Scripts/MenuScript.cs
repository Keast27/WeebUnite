using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public static MenuScript instance;
    public bool pause = false;

    public GameObject pauseUI;


    void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
    }

    public void Victory()
    {
        SceneManager.LoadScene("Victory");
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
