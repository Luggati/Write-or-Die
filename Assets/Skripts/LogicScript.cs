using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicScript : MonoBehaviour
{
    public GameObject deathScreen;
    public GameObject menu;
    bool isDead = false;
    bool isPaused = false;
    int leben = 3;
    int currentScore = 0;
    string scoreboard = "1000 Director Fury \n 604 Phil Collins \n 234 Anonymous";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (leben <= 0)
        {
            //TogglePause();
            deathScreen.SetActive(true);
            isDead = true;
        }
    }

    void OnGUI()
    {
        // pause with escape
        if (Event.current.Equals(Event.KeyboardEvent("escape")))
        {
            TogglePause();
            menu.SetActive(isPaused);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //TogglePause();
    }

    void TogglePause()
    {
        // Um zwischen pausiert und nicht pausiert zu wechseln
        isPaused = !isPaused;

        // Setze die Zeitachse entsprechend
        if (isPaused)
        {
            Time.timeScale = 0f; // Das Spiel ist pausiert
        }
        else
        {
            Time.timeScale = 1f; // Das Spiel läuft normal weiter
        }
    }

    public void QuitGame()
    {
        print("Exit Game");
        //Application.Quit();
    }

    public void DecreaseHealth()
    {
        if (leben > 0)
        {
            leben--;
        }
    }  
    
    public int GetHealth()
    {
        return leben;
    }

    public string GetScoreboard()
    {
        return scoreboard;
    }

    public void IncreaseScore(int score)
    {
        currentScore += score;
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

}

