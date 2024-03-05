using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavoir : MonoBehaviour
{
    public float movespeed = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left * movespeed) * Time.deltaTime;
    }
    public void DestroyIfMatches(char inputName)
    {
        if(inputName == gameObject.name[0])
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
        gameObject.name = randomLetter.ToString();
    }
}
