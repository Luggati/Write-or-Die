using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class UiSettings : MonoBehaviour
{
    public GameObject hud;
    public GameObject deathScreen;
    public GameObject menu;
    public GameObject currentInput;
    public GameObject logicScript;
    public GameObject startScreen;
    Color aktiveColor;
    Color inaktiveColor;


    List<GameObject> coreComp = new List<GameObject>();
    List<Text> textFields = new List<Text>();
    List<GameObject> weapons = new List<GameObject>();
    public Font font;

    // Start is called before the first frame update
    void Start()
    {
        weapons.Add(GameObject.Find("Weapon1"));
        weapons.Add(GameObject.Find("Weapon2"));
        weapons.Add(GameObject.Find("Weapon3"));

        coreComp.Add(hud);
        coreComp.Add(deathScreen);
        coreComp.Add(menu);
        coreComp.Add(currentInput);
        coreComp.Add(startScreen);


        foreach (GameObject go in coreComp)
        { 
            textFields.AddRange(go.GetComponentsInChildren<Text>());
        }


        SetTextColor(Color.red);

        ChangeFont(0);

    }

    // Update is called once per frame
    void Update()
    {
        ChangeFont(0);
    }

    void ChangeColor(Color color, List<Text> tfs)
    {
        foreach (Text tf in tfs)
        {
            tf.color = aktiveColor;
            if(tf.tag == "Inaktive")
            {
                tf.color = inaktiveColor;
            }
        }
    }

    void ChangeFont(int type)
    {
        foreach (Text tf in textFields)
        {
            tf.font = font;
        }
    }

    public void SetTextColor(Color color)
    {
        aktiveColor = new Color(color.r, color.g, color.b, 1);
        inaktiveColor = new Color(color.r, color.g, color.b, 0.3f);
        ChangeColor(color, textFields);
    }

    public void UpdateWeaponColor()
    {
        foreach (GameObject go in weapons)
        {
            if (go.tag == "Untagged")
            {
                go.GetComponent<Text>().color = aktiveColor;
            }
            else
            {
                go.GetComponent<Text>().color = inaktiveColor;
            }
        }
    }

    public Color GetActiveColor()
    {
        return aktiveColor;
    }
}
