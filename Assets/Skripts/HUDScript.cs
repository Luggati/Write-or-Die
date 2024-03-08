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
    public Text scoreboardTf;
    public InputField inputFied;
    public Text[] weapons;
    public Text lifecounterTF;
    public Text currentScore;

    // Start is called before the first frame update
    void Start()
    {
        scoreboardTf.text = logicScript.GetComponent<LogicScript>().GetScoreboard();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lifeCounter.text = "Leben: " + logicScript.GetComponent<LogicScript>().GetHealth();
        currentScore.text = "Your Score: " + logicScript.GetComponent<LogicScript>().GetCurrentScore();
    }


}
