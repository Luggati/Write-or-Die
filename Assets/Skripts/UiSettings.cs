using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

public class UiSettings : MonoBehaviour
{
    public Font font;
    public GameObject hud;
    public GameObject deathScreen;
    public GameObject menu;
    public GameObject currentInput;
    public GameObject logicScript;
    public GameObject startScreen;
    public GameObject weapon0;
    public GameObject weapon1;
    public GameObject weapon2;
    public GameObject lastInputs;
    public GameObject helpScreen;

    Color aktiveColor;
    Color inaktiveColor;


    List<GameObject> coreComp = new List<GameObject>();
    List<Text> textFields = new List<Text>();
    List<GameObject> weapons = new List<GameObject>();
    Queue<string> lastInputsQueue = new Queue<string>();
    

    // Start is called before the first frame update
    void Start()
    {
        weapons.Add(weapon0);
        weapons.Add(weapon1);
        weapons.Add(weapon2);

        coreComp.Add(hud);
        coreComp.Add(deathScreen);
        coreComp.Add(menu);
        coreComp.Add(currentInput);
        coreComp.Add(startScreen);
        coreComp.Add(lastInputs);
        coreComp.Add(helpScreen);


        foreach (GameObject go in coreComp)
        { 
            textFields.AddRange(go.GetComponentsInChildren<Text>());
        }

        
        LoadColor();

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
        }

        UpdateWeaponColor();
    }

    void LoadColor()
    {
        if (PlayerPrefs.HasKey("colorR"))
        {
            SetTextColor(new Color(PlayerPrefs.GetFloat("colorR"), PlayerPrefs.GetFloat("colorG"), PlayerPrefs.GetFloat("colorB"), 1));
        }
        else
        {
            SetTextColor(Color.white);
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
        weapon0.GetComponent<Text>().color = Color.yellow;
        weapon1.GetComponent<Text>().color = Color.cyan;
        weapon2.GetComponent<Text>().color = Color.red;

        foreach (GameObject go in weapons)
        {
            Color col = go.GetComponent<Text>().color;
            if (go.tag == "Inaktive")
            {
                go.GetComponent<Text>().color = new Color(col.r, col.g, col.b, 0.25f);
            }
        }
    }

    public Color GetActiveColor()
    {
        return aktiveColor;
    }

    public void UpdateLastInputs(string input)
    {
        lastInputsQueue.Enqueue(input);
        if (lastInputsQueue.Count > 3)
        {
            lastInputsQueue.Dequeue();
        }

        string[] inputs = lastInputsQueue.ToArray();
        string result = "";
        for (int i = 0; i < inputs.Length ; i++)
        {
            result += inputs[i] + "\n";
        }

        lastInputs.GetComponentInChildren<Text>().text = result;
    }

}
