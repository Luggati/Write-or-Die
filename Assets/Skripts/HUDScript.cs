using UnityEngine;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour
{

    public Text lifeCounter;
    public GameObject logicScript;
    public Text scoreboardTf;
    public InputField inputFied;
    public Text weapon0;
    public Text weapon1;
    public Text weapon2;
    public Text lifecounterTF;
    public Text currentScore;
    public Text commandListTf;
    public GameObject uiScript;
    public Text menuCommands;



    // Start is called before the first frame update
    void Start()
    {
        commandListTf.text = "\"help\" \n \"menu\" \n \"resume\" \n \"restart\" \n \"exit\" \n \"credits\" \n \n Weapons: \n Missile: \"m\" \n Laser: \"l\" \n Railgun: \"r\"";
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
        lifeCounter.text = "Health: " + logicScript.GetComponent<LogicScript>().GetHealth();
        currentScore.text = "Kills: " + logicScript.GetComponent<LogicScript>().GetCurrentScore();
        scoreboardTf.text = logicScript.GetComponent<LogicScript>().GetScoreboardAsString();

        Color color = uiScript.GetComponent<UiSettings>().GetActiveColor();
        string lang = logicScript.GetComponent<LogicScript>().GetLanguage();
        menuCommands.text = string.Format("Volume: {0} \n \"volume 100\" \n \n Textcolor: {1} {2} {3} \n \"color 255 255 255\" \n \n Enemy Words Language: {4} \n \"language [de,en]\"", AudioListener.volume * 100, (int) color.r*255, (int)color.g * 255, (int)color.b * 255, lang);
    }

}
