using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour
{

    public Text lifeCounter;
    public GameObject logicScript;
    public Text sbDiffTf;
    public Text scoreboardTf;
    public InputField inputFied;
    public Text weapon0;
    public Text weapon1;
    public Text weapon2;
    public Text lifecounterTF;
    public Text scrapTf;
    public Text currentScore;
    public Text commandListTf;
    public GameObject uiScript;
    public Text menuCommands;



    // Start is called before the first frame update
    void Start()
    {
        commandListTf.text = "\"help\"\n\"menu\"\n\"resume\"\n\"restart\"\n\"exit\"\n\"credits\"\n\nWeapons:\nMissile:\"[1,m]\"\nLaser:\"[2,l]\"\n Railgun:\"[3,r]\"";
        UpdateText();
    }

    void Update()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateText();
    }

    public void ChangeWeapon(int type)
    {
        if (type == 0)
        {
            weapon0.tag = "Untagged";
            weapon1.tag = "Inaktive";
            weapon2.tag = "Inaktive";
        }
        else if (type == 1)
        {
            weapon0.tag = "Inaktive";
            weapon1.tag = "Untagged";
            weapon2.tag = "Inaktive";
        }
        else if (type == 2)
        {
            weapon2.tag = "Untagged";
            weapon0.tag = "Inaktive";
            weapon1.tag = "Inaktive";
        }
        uiScript.GetComponent<UiSettings>().UpdateWeaponColor();
    }

    public void UpdateText()
    {
        LogicScript ls = logicScript.GetComponent<LogicScript>();
        lifeCounter.text = "Health: " + ls.GetHealth();
        currentScore.text = "Kills: " + ls.GetCurrentScore();
        scoreboardTf.text = ls.GetScoreboardAsString();
        sbDiffTf.text = "(Difficulty: " + ls.GetDifficulty() + ")";
        scrapTf.text = "Scrap: " + ls.GetScrap() + "\nHexcores: " + ls.GetHexcores();


        Color color = uiScript.GetComponent<UiSettings>().GetActiveColor();
        string colorString = string.Format("{0} {1} {2}", (int)color.r * 255, (int)color.g * 255, (int)color.b * 255);
        string lang = ls.GetLanguage();
        int diff = ls.GetDifficulty();
        menuCommands.text = string.Format("Volume: {0} \n \"volume 100\" \n \n Textcolor: {1} \n \"color 255 255 255\" \n \n Enemy Words Language: {2} \n \"language [de,en]\" \n \n Difficulty: {3} \n \"difficulty [1,4]\" ", 
            AudioListener.volume * 100, colorString , lang, diff);
    }

}
