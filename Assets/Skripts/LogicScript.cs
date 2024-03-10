using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{
    public GameObject deathScreen;
    public GameObject menu;
    public GameObject hud;
    public InputField inputField;
    public GameObject cam;
    public GameObject enemyHandler;
    public GameObject ui;
    bool isDead = false;
    
    int Health = 3;
    int currentScore = 0;
    string scoreboard = "1000 Director Fury \n 604 Phil Collins \n 234 Anonymous";
    string language = "en";
    List<string> prohibitedWords = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        SetProhibitedWords();
        Pause();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            //TogglePause();
            deathScreen.GetComponent<DeathScreenScript>().SetCurrentScore();
            deathScreen.SetActive(true);
            isDead = true;
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            string input = inputField.text;
            string[] inputPieces = input.Split(' ');
            switch(inputPieces[0])
            {
                case "menu":
                    ToggleMenu(true);
                    break;
                case "exit":
                    QuitGame();
                    break;
                case "restart":
                    RestartGame();
                    break;
                case "help":
                    //TODO: create Tutorial-Screen
                    break;
                case "start":
                case "resume":
                    ToggleMenu(false);
                    //TODO: Toggle help-Menu
                    break;
                //TODO: case um seinen score im scoreboard mit seinem Namen hinzuzufügen
                case "L":
                case "R":
                case "M":
                    hud.GetComponent<HUDScript>().ChangeWeapon(inputField.text);
                    break;
                case "volume":
                    if (inputPieces.Length == 2 && menu.activeSelf == true)
                    {
                        if (int.TryParse(inputPieces[1], out int zahl))
                        {
                            if (zahl > 100 || zahl < 0)
                            {
                                break;
                            }
                            AudioListener.volume = (float) zahl/100;
                        } 
                    }
                    break;
                case "color":
                    if (inputPieces.Length == 4 && menu.activeSelf == true)
                    {
                        if (int.TryParse(inputPieces[1], out int x) && int.TryParse(inputPieces[2], out int y) && int.TryParse(inputPieces[3], out int z))
                        {
                            if (x > 255 || x < 0 || y > 255 || y < 0 || z > 255 || z < 0)
                            {
                                break;
                            }
                            Color color = new Color(x/255f, y/255f, z/255f, 1f);
                            ui.GetComponent<UiSettings>().SetTextColor(color);
                        }
                        
                    }
                    break;
                case "language":
                    if (inputPieces.Length == 2 && menu.activeSelf == true)
                    {
                        if (inputPieces[1].Equals("de") || inputPieces[1].Equals("en"))
                        {
                            language = inputPieces[1];
                        }
                    }
                    break;

                default:
                    if (isDead == false)
                    {
                        enemyHandler.GetComponent<EnemyHandler>().CheckInput(inputField.text);
                    }
                    break;
            }
            
            inputField.text = ""; // Setze das Input Field zurück
            inputField.ActivateInputField(); // Setze den Fokus zurück auf das Input Field
            inputField.Select(); // Wähle das Input Field aus, um sicherzustellen, dass der Cursor sichtbar bleibt

            hud.GetComponent<HUDScript>().UpdateText();
        }
    }

    void SetProhibitedWords()
    {
        prohibitedWords.Add("menu");
        prohibitedWords.Add("exit");
        prohibitedWords.Add("restart");
        prohibitedWords.Add("help");
        prohibitedWords.Add("start");
        prohibitedWords.Add("resume");
        prohibitedWords.Add("volume");
        prohibitedWords.Add("color");
        prohibitedWords.Add("language");
    }


    void RestartGame()
    {
        EnemyBehavoir enemys = FindObjectOfType<EnemyBehavoir>();
        foreach (EnemyBehavoir enemy in FindObjectsOfType<EnemyBehavoir>())
        {
            Destroy(enemy.gameObject);
        }
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        deathScreen.SetActive(false);
        currentScore = 0;
        Health = 3;
        isDead = false;
        ToggleMenu(false);
    }

    void ToggleMenu(bool enableMenu)
    {
        if (enableMenu)
        {
            Pause();
            menu.SetActive(true);
        } else
        {
            menu.SetActive(false);
            hud.SetActive(true);
            UnPause();
        }
    }

    public void DecreaseHealth()
    {
            if (Health > 0)
            {
                Health--;
            }
    }  
    
    public int GetHealth()
    {
        return Health;
    }

    public string GetScoreboard()
    {
        return scoreboard;
    }

    public void IncreaseScore(int score)
    {
        if (isDead == false)
        {
            currentScore += score;
        }
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public List<string> GetProhibitedWords()
    {
        return prohibitedWords;
    }


    void Pause()
    {
        Time.timeScale = 0f;
    }

    void UnPause()
    {
        Time.timeScale = 1.0f;
    }

    public void QuitGame()
    {
        print("Exit Game");
        //Application.Quit();
    }

    public string GetLanguage()
    {
        return language;
    }

}

