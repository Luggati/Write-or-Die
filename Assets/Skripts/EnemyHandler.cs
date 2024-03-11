using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
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
    private float timer = 2;
    float spawnOffset = 14;
    
    // Start is called before the first frame update
    void Start()
    {

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
        float lowestPoint = transform.position.y - spawnOffset + 2.0f;
        float highestPoint = transform.position.y + spawnOffset;

        Vector3 direction = Vector3.Normalize(new Vector3(5, 0, 0) - transform.position);
        GameObject enemy = Instantiate(enemys, 
            new Vector3(transform.position.x, 
            Random.Range(lowestPoint, highestPoint), 0), 
            Quaternion.LookRotation(Vector3.forward, direction));
        enemy.GetComponent<EnemyBehavoir>().SetEnemyDirection(direction);
        enemy.GetComponent<EnemyBehavoir>().SetTextColor(GameObject.Find("UI").GetComponent<UiSettings>().GetActiveColor());

    }

    public void CheckInput(string userInput)
    {
        // Finde alle Gegner im Spiel
        EnemyBehavoir[] enemies = FindObjectsOfType<EnemyBehavoir>();
        string firstLetter = userInput[0].ToString();
        string restOfWord = userInput[1..];

        foreach (EnemyBehavoir enemy in enemies)
        {
            // Vergleiche den User-Input mit jedem Gegner-Wort
            if (restOfWord.Equals(enemy.GetEnmyText(), System.StringComparison.OrdinalIgnoreCase) 
                && int.TryParse(firstLetter, out int shotType))
            {
                //TODO: Waffenauswahl derzeit it 1,2,3. Wenn mit Buchstaben, if-Abfrage hier einfügen
                //z.B. if (firstLetter == "a" -> shotType = 0-2)
                gun.GetComponent<GunScript>().Fire(enemy, shotType - 1);
                
                inputField.text = ""; // Lösche das InputField für die nächste Eingabe
                return; // Beende die Schleife, da das Wort gefunden wurde
            }
        }
    }

}
