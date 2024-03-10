using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class EnemyHandler : MonoBehaviour
{
    public GameObject enemys;
    public InputField inputField;
    public GameObject gun;
    public GameObject logicScript;
    public GameObject utils;

    public float spawnrate = 2;
    private float timer = 3;
    float spawnOffset = 14;
    
    // Start is called before the first frame update
    void Start()
    {
        inputField.text = "";
        //spawnEnemy();
        inputField.ActivateInputField();

    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnrate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            spawnEnemy();
            timer = 0;
        }

    }

    public void spawnEnemy()
    {
        float lowestPoint = transform.position.y - spawnOffset + 1.7f;
        float highestPoint = transform.position.y + spawnOffset;

        GameObject enemy = Instantiate(enemys, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), Quaternion.identity);
        enemy.transform.up = -transform.right;

        if (logicScript.GetComponent<LogicScript>().GetLanguage().Equals("en"))
        {
            enemy.GetComponentInChildren<Text>().text = utils.GetComponent<UtilsScript>().GetRandomEngWordWithLenght(3).ToLower();
        }
        else
        {
            enemy.GetComponentInChildren<Text>().text = utils.GetComponent<UtilsScript>().GetRandomGerWordWithLenght(3).ToLower();
        }

    }

    public void CheckInput(string userInput)
    {
        // Finde alle Gegner im Spiel
        EnemyBehavoir[] enemies = FindObjectsOfType<EnemyBehavoir>();

        foreach (EnemyBehavoir enemy in enemies)
        {
            // Vergleiche den User-Input mit jedem Gegner-Wort
            if (userInput.Equals(enemy.textname.text, System.StringComparison.OrdinalIgnoreCase))
            {
                gun.GetComponent<GunScript>().Fire(enemy);
                /*
                 // Füge einen Punkt zum Score hinzu
                Destroy(enemy.gameObject); // Zerstöre den Gegner, wenn das Wort übereinstimmt
                */
                inputField.text = ""; // Lösche das InputField für die nächste Eingabe
                return; // Beende die Schleife, da das Wort gefunden wurde
            }
        }
    }

    public void Hit(GameObject enemy)
    {
        logicScript.GetComponent<LogicScript>().IncreaseScore(1);
        Destroy(enemy);
    }
}
