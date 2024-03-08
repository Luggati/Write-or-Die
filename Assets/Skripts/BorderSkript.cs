using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderSkript : MonoBehaviour
{

    public GameObject logicScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.CompareTag("Enemy"))
        {
            logicScript.GetComponent<LogicScript>().DecreaseHealth();
            Destroy(col.gameObject);
        }
    }
}
