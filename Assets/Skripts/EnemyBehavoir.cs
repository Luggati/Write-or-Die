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
        SetTextPos();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.left * movespeed) * Time.deltaTime;
        SetTextPos();
    }
    public void DestroyIfMatches(char inputName)
    {
        if (inputName == null)
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
        print("randomname geht");
        //Vector3 namePosition = transform.position;
        //namePosition.y -= GetComponent<Renderer>().bounds.extents.y; // Platzieren Sie den Namen am unteren Rand des Gegners
        //textname.transform.position = namePosition;
    }
    public void SetTextPos()
    {
        if (textname != null)
        {
            textname.transform.position = transform.position;
        }
    }

}
