using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class HUDScript : MonoBehaviour
{

    public Text lifeCounter;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lifeCounter.text = "Leben: " + player.GetComponent<PlayerScript>().GetHealth();
        

    }


}
