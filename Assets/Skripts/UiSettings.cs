using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class UiSettings : MonoBehaviour
{
    public GameObject hud;
    public GameObject deathScreen;
    public GameObject menu;
    public GameObject inputField;
    public GameObject logicScript;
    Color aktiveColor;
    Color inaktiveColor;


    List<GameObject> coreComp = new List<GameObject>();
    List<Text> textFields = new List<Text>();
    List<GameObject> weapons = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        weapons.Add(GameObject.Find("Weapon1"));
        weapons.Add(GameObject.Find("Weapon2"));
        weapons.Add(GameObject.Find("Weapon3"));

        coreComp.Add(hud);
        coreComp.Add(deathScreen);
        coreComp.Add(menu);
        coreComp.Add(inputField);


        foreach (GameObject go in coreComp)
        { 
            textFields.AddRange(go.GetComponentsInChildren<Text>());
        }
        SetTextColor(Color.red);

    }

    // Update is called once per frame
    void Update()
    {
        
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
