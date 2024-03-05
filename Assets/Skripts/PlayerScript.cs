using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    int health = 3;
    public GameObject deathScreen;

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
            health--;
            if (health <= 0)
            {
                showDeathScreen();
            }
        }
    }

    public int GetHealth()
    {
        return health;
    }

    void showDeathScreen()
    {
        deathScreen.SetActive(true);
    }
}
