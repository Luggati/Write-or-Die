using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static LogicScript;

public class LogicScript : MonoBehaviour
{
    public GameObject deathScreen;
    public GameObject menu;
    public GameObject hud;
    public GameObject help;
    public InputField inputField;
    public GameObject cam;
    public GameObject enemyHandler;
    public GameObject ui;
    public GameObject startScreen;
    public GameObject currentInputUi;
    public GameObject player;
    public GameObject gun;
    bool isDead = false;
    
    int Health = 3;
    int currentScore = 0;
    ScoreBoard sb;
    string language = "en";
    bool scoreAdded = false;
    List<string> prohibitedWords = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        sb = new ScoreBoard();
        sb.LoadScoreboard();

        Pause();
        LoadPlayerPrefs();

        currentInputUi.SetActive(false);
        hud.SetActive(false);
        menu.SetActive(false);
        deathScreen.SetActive(false);
        startScreen.SetActive(true);
        currentInputUi.SetActive(true);

        SetProhibitedWords();
        inputField.text = "";
        inputField.ActivateInputField();
    }


    // Update is called once per frame
    void Update()
    {
        if (Health <= 0 && isDead == false)
        {
            //TogglePause();
            player.GetComponent<PlayerScript>().KillPlayer();
            deathScreen.GetComponent<DeathScreenScript>().SetCurrentScore();
            deathScreen.SetActive(true);
            isDead = true;
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            CheckInput();
        }
    }

    void CheckInput()
    {
        string input = inputField.text;
        ui.GetComponent<UiSettings>().UpdateLastInputs(inputField.text);
        string[] inputPieces = input.Split(' ');
        switch (inputPieces[0])
        {
            case "menu":
                ToggleMenu(true, menu);
                break;
            case "exit":
                QuitGame();
                break;
            case "restart":
                RestartGame();
                break;
            case "help":
                ToggleMenu(true, help);
                break;
            case "start":
            case "resume":
                ToggleMenu(false, null);
                break;
            //TODO: case um seinen score im scoreboard mit seinem Namen hinzuzufügen
            case "1":
            case "2":
            case "3":
            case "l":
            case "r":
            case "m":
                ChangeWeapon(inputPieces);
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
                        AudioListener.volume = (float)zahl / 100;
                        PlayerPrefs.SetFloat("volume", AudioListener.volume);
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
                        Color color = new Color(x / 255f, y / 255f, z / 255f, 1f);
                        ui.GetComponent<UiSettings>().SetTextColor(color);
                        PlayerPrefs.SetFloat("colorR", color.r);
                        PlayerPrefs.SetFloat("colorG", color.g);
                        PlayerPrefs.SetFloat("colorB", color.b);
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
                    PlayerPrefs.SetString("language", language);
                }
                break;
            case "score":
                if (inputPieces.Length >= 2 && deathScreen.activeSelf == true && scoreAdded == false)
                {
                    sb.AddToScoreboard(inputPieces[1], currentScore);
                    scoreAdded = true;
                }
                
                break;
            default:
                if (isDead == false)
                {
                    enemyHandler.GetComponent<EnemyHandler>().CheckInput(inputField.text);
                }
                break;
        }

        PlayerPrefs.SetString("settings", "volume colorR colorG colorB settings language");
        PlayerPrefs.Save();
        
        inputField.text = ""; // Setze das Input Field zurück
        inputField.ActivateInputField(); // Setze den Fokus zurück auf das Input Field
        inputField.Select(); // Wähle das Input Field aus, um sicherzustellen, dass der Cursor sichtbar bleibt

        hud.GetComponent<HUDScript>().UpdateText();
        
    }

    void ChangeWeapon(string[] input)
    {
        int type = ConvertInputToWeapon(input[0]);

        if (type == -1)
        {
            return;
        }

        hud.GetComponent<HUDScript>().ChangeWeapon(type);
        gun.GetComponent<GunScript>().SetWeaponType(type);

        if (input.Length == 2) 
        {
            if (isDead == false)
            {
                enemyHandler.GetComponent<EnemyHandler>().CheckInput(input[1]);
            }
        }
    }

    int ConvertInputToWeapon(string input)
    {
        if (input.Equals("l"))
        {
            return 1;
        }
        else if (input.Equals("r"))
        {
            return 2;
        }
        else if (input.Equals("m"))
        {
            return 0;
        }
        else if (int.TryParse(input, out int type))
        {
            return type - 1;
        }
        else
        {
            return -1;
        }
    }

    void RestartGame()
    {
        player.GetComponent<PlayerScript>().ResetPlayer();
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
        ToggleMenu(false, null);
        scoreAdded = false;
    }

    void ToggleMenu(bool enableMenu, GameObject go)
    {
        if (enableMenu)
        {
            Pause();
            hud.SetActive(false);
            currentInputUi.SetActive(false);
            go.SetActive(true);
            hud.SetActive(true);
            currentInputUi.SetActive(true);
        } else
        {
            menu.SetActive(false);
            startScreen.SetActive(false);
            help.SetActive(false);
            hud.SetActive(true);
            UnPause();
        }
    }

    void LoadPlayerPrefs()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
        language = PlayerPrefs.GetString("language");
    }


    // Public Methods

    public void DecreaseHealth()
    {
        if (Health > 0)
        {
            Health--;
        }
    }

    public void IncreaseScore(int score)
    {
        if (isDead == false)
        {
            currentScore += score;
        }
    }


    // Getter and Setter

    public int GetCurrentScore()
    {
        return currentScore;
    }

    public List<string> GetProhibitedWords()
    {
        return prohibitedWords;
    }

    public int GetHealth()
    {
        return Health;
    }

    public string GetScoreboardAsString()
    {
        return sb.GetScoreboardAsString();
    }

    public string GetLanguage()
    {
        return language;
    }


    // Utils
    void Pause()
    {
        Time.timeScale = 0f;
    }

    void UnPause()
    {
        Time.timeScale = 1.0f;
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

    public void QuitGame()
    {
        print("Exit Game");
        //Application.Quit();
    }

    //[Serializable]
    public class ScoreBoard
    {
        private class Entry
        {
            public string name;
            public int score;

            public Entry(string name, int score)
            {
                this.name = name;
                this.score = score;
            }
        }


        List<Entry> scoreboard = new List<Entry>();

        
        public void AddToScoreboard(string name, int score)
        {
            Entry entry = new Entry(name, score);
            scoreboard.Add(entry);
            print("Raw: " + GetScoreboardAsString());
            SortScoreboard();
            print("Cut: " + GetScoreboardAsString());
            SaveScoreboard();
            
        }

        void SortScoreboard()
        {
            List<Entry> sortedList = scoreboard.OrderByDescending(x => x.score).ToList();
            scoreboard = sortedList;
            print("Sorted: " + GetScoreboardAsString());
            if (scoreboard.Count > 5)
            {
                scoreboard = scoreboard.GetRange(0, 5);
            }
        }

        public string GetScoreboardAsString()
        {
            string result = "";
            foreach (Entry entry in scoreboard)
            {
                result += " " + entry.name + ":  " + entry.score + "\n ";
            }

            return result;
        }

        void SaveScoreboard()
        {
            string path = Application.persistentDataPath + "scoreboard.json";
            string json = JsonUtility.ToJson(scoreboard);
            File.WriteAllText(path, json);
        }

        public void LoadScoreboard()
        {
            string path = Application.persistentDataPath + "scoreboard.json";
            if (File.Exists(path))
            {
                string json = File.ReadAllText(path);
                scoreboard = JsonUtility.FromJson<List<Entry>>(json);
            }
            else
            {
                scoreboard = new List<Entry>();
            }
        }


    }

}

