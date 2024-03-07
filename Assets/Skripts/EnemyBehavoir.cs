using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBehavoir : MonoBehaviour
{
    public float movespeed = 5;
    public Text textname;


    // Start is called before the first frame update
    void Start()
    {
        SetRandomName();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left * movespeed) * Time.deltaTime;
        

        Vector3 pos = transform.position;
        if (pos.x < -28 || pos.x > 28 || pos.y < -15 || pos.y > 15)
        {
            Destroy(gameObject);
        }
    }
 
    private char GetRandomLetter()
    {
        // Verwende ASCII-Werte für Großbuchstaben (65-90)
        int randomInt = Random.Range(65, 91);
        char randomChar = (char)randomInt;
        return randomChar;
    }
    public void SetRandomName()
    {
        char randomLetter = GetRandomLetter();
        textname.text = randomLetter.ToString();
        for (int i = 0; i < 4; i++)
        {
            if (Random.Range(0, 2) == 0)
            {
                char letter = GetRandomLetter();
                textname.text = textname.text + letter.ToString();
            }
        }
    }
  

}
