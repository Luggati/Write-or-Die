using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreenScript : MonoBehaviour
{

    public GameObject logicScript;
    public Text scoreboardTf;
    // Start is called before the first frame update
    void Start()
    {
        scoreboardTf.text = logicScript.GetComponent<LogicScript>().GetScoreboard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
