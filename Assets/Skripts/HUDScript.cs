using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Unity.VisualScripting;

public class HUDScript : MonoBehaviour
{

    public Text lifeCounter;
    public GameObject logicScript;
    public Text scoreboardTf;
    public InputField inputFied;
    public Text weapon1;
    public Text weapon2;
    public Text weapon3;
    public Text lifecounterTF;
    public Text currentScore;
    public Text commandListTf;
    public GameObject uiScript;
    public Text menuCommands;

    // Start is called before the first frame update
    void Start()
    {
        scoreboardTf.text = logicScript.GetComponent<LogicScript>().GetScoreboard();
        commandListTf.text = "\"help\" \n \"menu\" \n \"resume\" \n \"restart\" \n \"exit\" \n \n Weapons: \n Laser 1: \"L\" \n Railgun 2: \"R\" \n Missile 3: \"M\"";
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

    public void ChangeWeapon(string c)
    {
        if (c.Equals("L"))
        {
            weapon1.tag = "Untagged";
            weapon2.tag = "Inaktive";
            weapon3.tag = "Inaktive";
        }
        else if (c.Equals("R"))
        {
            weapon1.tag = "Inaktive";
            weapon2.tag = "Untagged";
            weapon3.tag = "Inaktive";
        }
        else if (c.Equals("M"))
        {
            weapon3.tag = "Untagged";
            weapon1.tag = "Inaktive";
            weapon2.tag = "Inaktive";
        }
        uiScript.GetComponent<UiSettings>().UpdateWeaponColor();
    }

    public void UpdateText()
    {
        lifeCounter.text = "Health: " + logicScript.GetComponent<LogicScript>().GetHealth();
        currentScore.text = "Your Score: " + logicScript.GetComponent<LogicScript>().GetCurrentScore();

        Color color = uiScript.GetComponent<UiSettings>().GetActiveColor();
        string lang = logicScript.GetComponent<LogicScript>().GetLanguage();
        menuCommands.text = string.Format("Volume: {0} \n \"volume 100\" \n \n Textcolor: {1} {2} {3} \n \"color 255 255 255\" \n \n Enemy Words Language: {4} \n \"language [de,en]\"", AudioListener.volume * 100, (int) color.r*255, (int)color.g * 255, (int)color.b * 255, lang);
    }

}
