using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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


    List<GameObject> coreComp = new List<GameObject>();
    Text[] textFields;
    Color color;

    // Start is called before the first frame update
    void Start()
    {
        coreComp.Add(hud);
        coreComp.Add(deathScreen);
        coreComp.Add(menu);
        coreComp.Add(inputField);

        foreach (GameObject go in coreComp)
        {
            textFields = go.GetComponentsInChildren<Text>();

            ChangeColor(Color.red, textFields);
        }

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeColor(Color color, Text[] tfs)
    {
        foreach (Text tf in tfs)
        {
            tf.color = color;
        }
    }
}
