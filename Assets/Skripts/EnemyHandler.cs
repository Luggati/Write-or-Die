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

    public float spawnInterval = 2.2f;
    private float timer = 2;
    float spawnOffset = 12;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnInterval)
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
        float lowestPoint = transform.position.y - spawnOffset;
        float highestPoint = transform.position.y + spawnOffset;

        Vector3 direction = Vector3.Normalize(new Vector3(5, 0, 0) - transform.position);
        GameObject enemy = Instantiate(enemys, 
                new Vector3(transform.position.x, 
                Random.Range(lowestPoint, highestPoint), 0), 
                Quaternion.LookRotation(Vector3.forward, direction));

        enemy.GetComponent<EnemyBehavoir>().SetEnemyDirection(direction);
        enemy.GetComponent<EnemyBehavoir>().SetTextColor(GameObject.Find("UI").GetComponent<UiSettings>().GetActiveColor());

        spawnInterval = spawnInterval - 0.01f;
    }

    public void CheckInput(string userInput)
    {
        // Finde alle Gegner im Spiel
        EnemyBehavoir[] enemies = FindObjectsOfType<EnemyBehavoir>();

        foreach (EnemyBehavoir enemy in enemies)
        {
            // Vergleiche den User-Input mit jedem Gegner-Wort
            if (userInput.Equals(enemy.GetEnmyText(), System.StringComparison.OrdinalIgnoreCase))
            {
                gun.GetComponent<GunScript>().Fire(enemy);
                
                inputField.text = ""; // Lösche das InputField für die nächste Eingabe
                return; // Beende die Schleife, da das Wort gefunden wurde
            }
        }
    }

}
