using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour
{

    public Text lifeCounter;
    public GameObject logicScript;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lifeCounter.text = "Leben: " + logicScript.GetComponent<LogicScript>().GetHealth();
    }


}
