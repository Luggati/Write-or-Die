using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHandler : MonoBehaviour
{
    public GameObject enemys;
    public InputField inputField;
    public GameObject gun;
    public GameObject logicScript;
    public GameObject utils;

    float startSpawnInterval = 3.2f;
    float spawnIntervallDiffFactor = 0.4f;
    float spawnIntervalOverTimeFactor = 0.005f;
    public float spawnInterval = 2.5f;

    private float timer = 10;
    float spawnOffset = 12;
    int difficulty = 2;

    List<Vector3> activeSpawner = new List<Vector3>();
    
    // Start is called before the first frame update
    void Start()
    {
        spawnInterval = startSpawnInterval - 0.5f * difficulty;
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
            spawnEnemy(transform.position, true);

            foreach(Vector3 spawner in activeSpawner)
            {
                spawnEnemy(spawner, false);
            }

            if (spawnInterval > 0.5f)
            {
                spawnInterval = spawnInterval - spawnIntervalOverTimeFactor;
            }
            
            timer = 0;
        }
    }

    public void spawnEnemy(Vector3 spawnPos, bool isNatural)
    {
        if (isNatural)
        {
            spawnPos.y = Random.Range(spawnPos.y - spawnOffset, spawnPos.y + spawnOffset);
        }

        Vector3 direction = Vector3.left;
        GameObject enemy = Instantiate(enemys, spawnPos, Quaternion.LookRotation(Vector3.forward, direction));

        if (enemy.GetComponent<EnemyBehavoir>().GetEnemyType() == 5)
        {
            int sign = (Random.value < 0.5f) ? 1: -1;
            Vector3 enemyConstPos = spawnPos;
            enemyConstPos.y = sign * Random.Range(spawnPos.y + spawnOffset / 2 + 2, spawnPos.y + spawnOffset);
            enemy.transform.position = enemyConstPos;
        }

        enemy.GetComponent<EnemyBehavoir>().SetEnemyDirection(direction);
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

    public void AddActiveSpawner(Vector3 spawnerPos)
    {
        activeSpawner.Add(spawnerPos);
    }

    public void RemoveActiveSpawner(Vector3 spawnerPos)
    {
        activeSpawner.Remove(spawnerPos);
    }

    public void UpdateDifficulty(int diff)
    {
        difficulty = diff;

        // SpawnInterval
        if (diff < 3)
        {
            spawnInterval = startSpawnInterval - spawnIntervallDiffFactor * diff;
            spawnIntervalOverTimeFactor = diff * 0.01f;
        }
        else
        {
            spawnInterval = startSpawnInterval - spawnIntervallDiffFactor * (diff - 1);
            spawnIntervalOverTimeFactor = (diff - 1) * 0.01f;
        }

    }

    public int GetDifficulty()
    {
        return difficulty;
    }

    public void ResetEnemyHandler()
    {
        activeSpawner = new List<Vector3>();
        UpdateDifficulty(difficulty);
    }
}
