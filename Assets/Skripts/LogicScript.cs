using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

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
    public GameObject credits;
    public GameObject startScreen;
    public GameObject currentInputUi;
    public GameObject player;
    public GameObject gun;
    bool isDead = false;
    
    int Health = 3;
    int currentScore = 0;
    int scrap = 0;
    int hexcores = 0;
    int difficulty = 2;
    int maxDifficulty = 4;
    ScoreBoard sb;
    string language = "en";
    string playerName;
    bool scoreAdded = false;
    List<string> prohibitedWords = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        Pause();
        LoadPlayerPrefs();

        sb = new ScoreBoard(maxDifficulty);
        sb.LoadScoreboard();

        enemyHandler.GetComponent<EnemyHandler>().UpdateDifficulty(difficulty);

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
            string input = inputField.text.TrimStart();
            ui.GetComponent<UiSettings>().UpdateLastInputs(input);

            if (startScreen.activeSelf)
            {
                playerName = input;
                ToggleMenu(true, help);
            }
            else
            {
                CheckInput(input);
            }
            ResetInputField();
        }
    }

    void CheckInput(string input)
    {
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
            case "credits":
                ToggleMenu(true, credits);
                break;
            case "1":
            case "2":
            case "3":
            case "l":
            case "r":
            case "m":
                ChangeWeapon(inputPieces);
                break;
            case "difficulty":
                if (inputPieces.Length == 2 && menu.activeSelf == true)
                {
                    if (int.TryParse(inputPieces[1], out int zahl))
                    {
                        difficulty = Mathf.Clamp(zahl, 1, 4);
                        PlayerPrefs.SetInt("difficulty", difficulty);
                        enemyHandler.GetComponent<EnemyHandler>().UpdateDifficulty(difficulty);
                    }
                }
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
                if (deathScreen.activeSelf == true && scoreAdded == false)
                {
                    sb.AddToScoreboard(playerName, currentScore, difficulty);
                    scoreAdded = true;
                }
                
                break;
            default:
                if (isDead == false)
                {
                    enemyHandler.GetComponent<EnemyHandler>().CheckInput(input);
                }
                break;
        }

        PlayerPrefs.SetString("settings", "volume colorR colorG colorB settings language");
        PlayerPrefs.Save();
    }

    void ResetInputField()
    {
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
        enemyHandler.GetComponent<EnemyHandler>().ResetEnemyHandler();
        EnemyBehavoir enemys = FindObjectOfType<EnemyBehavoir>();
        foreach (EnemyBehavoir enemy in FindObjectsOfType<EnemyBehavoir>())
        {
            Destroy(enemy.gameObject);
        }
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        deathScreen.SetActive(false);
        currentScore = 0;
        Health = 3;
        enemyHandler.GetComponent<EnemyHandler>().spawnInterval = 2.2f;
        isDead = false;
        ChangeWeapon(new string[]{"m"});
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
            menu.SetActive(true);
            help.SetActive(false);
            credits.SetActive(false);
            startScreen.SetActive(false);
            go.SetActive(true);
            hud.SetActive(true);
            currentInputUi.SetActive(true);
        } else
        {
            menu.SetActive(false);
            startScreen.SetActive(false);
            help.SetActive(false);
            credits.SetActive(false);
            hud.SetActive(true);
            UnPause();
        }
    }

    void LoadPlayerPrefs()
    {
        if (PlayerPrefs.HasKey("volume") == false)
        {
            PlayerPrefs.SetFloat("volume", 1.0f);
        } 
        else
        {
            AudioListener.volume = PlayerPrefs.GetFloat("volume");
        }

        if (PlayerPrefs.HasKey("language") == false)
        {
            PlayerPrefs.SetString("language", "en");
        }
        else
        {
            language = PlayerPrefs.GetString("language");
        }

        if (PlayerPrefs.HasKey("difficulty") == false)
        {
            PlayerPrefs.SetInt("difficulty", 2);
        }
        else
        {
            difficulty = PlayerPrefs.GetInt("difficulty");
        }
    }


    // Public Methods

    public void DecreaseHealth()
    {
        if (Health > 0)
        {
            Health--;
        }
    }

    public void IncreaseHealth(int amount)
    {
        Health += amount;
    }

    public void IncreaseScore(int score)
    {
        if (isDead == false)
        {
            currentScore += score;
            if (currentScore >= 5 && currentScore % 5 == 0)
            {
                scrap++;
            }
        }
    }

    public void AddHexcores(int amount)
    {
        hexcores += amount;
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
        return sb.GetScoreboardAsString(difficulty);
    }

    public string GetLanguage()
    {
        return language;
    }

    public int GetDifficulty()
    {
        return difficulty;
    }

    public int GetScrap()
    {
        return scrap;
    }

    public int GetHexcores()
    {
        return hexcores;
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
        prohibitedWords.Add("difficulty");
    }

    public void QuitGame()
    {
        print("Exit Game");
        Application.Quit();
    }

    [Serializable]
    public class ScoreBoard
    {
        [Serializable]
        public class Entry
        {
            public string name;
            public int score;

            public Entry()
            {

            }

            public Entry(string name, int score)
            {
                this.name = name;
                this.score = score;
            }
        }


        List<Entry> scoreboard = new List<Entry>();
        List<Entry> sbMixed = new List<Entry>();
        List<List<Entry>> scoreboards = new List<List<Entry>>();

        int maxDiff;

        public ScoreBoard (int maxDiff)
        {
            this.maxDiff = maxDiff;
            FillScorebaords();
        }
        

        public void AddToScoreboard(string name, int score, int difficulty)
        {
            Entry entry = new Entry(name, score);
            scoreboards[difficulty].Add(entry);
            SortScoreboard(difficulty);
            SaveScoreboard();
            
        }

        void SortScoreboard(int difficulty)
        {
            List<Entry> sortedList = scoreboards[difficulty].OrderByDescending(x => x.score).ToList();
            scoreboards[difficulty] = sortedList;
            if (scoreboards[difficulty].Count > 5)
            {
                scoreboards[difficulty] = scoreboards[difficulty].GetRange(0, 5);
            }
        }

        public string GetScoreboardAsString(int difficulty)
        {
            string result = "";
            foreach (Entry entry in scoreboards[difficulty])
            {
                result += entry.name + ":  " + entry.score + "\n";
            }

            return result;
        }

        void SaveScoreboard()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<List<Entry>>));

            string path = Application.persistentDataPath + "scoreboards.xml";
            TextWriter writer = new StreamWriter(path);
            serializer.Serialize(writer, scoreboards);
            writer.Close();
        }

        public void LoadScoreboard()
        {
            string path = Application.persistentDataPath + "scoreboards.xml";
            if (File.Exists(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(List<List<Entry>>));
                TextReader reader = new StreamReader(path);
                scoreboards = (List<List<Entry>>)serializer.Deserialize(reader);
                reader.Close();
            }
            else
            {
                scoreboards = new List<List<Entry>>();
                FillScorebaords();
            }
        }

        void FillScorebaords()
        {
            for (int i = 0; i <= maxDiff; i++)
            {
                scoreboards.Add(new List<Entry>());
            }
        }


    }

}

