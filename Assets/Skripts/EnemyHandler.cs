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

    public float spawnrate = 2;
    private float timer = 0;
    float spawnOffset = 14;
    
    // Start is called before the first frame update
    void Start()
    {
        inputField.text = "";
        spawnEnemy();
        inputField.onEndEdit.AddListener(value => {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                CheckInput(value); // Überprüfe die Eingabe
                inputField.text = ""; // Setze das Input Field zurück
                inputField.ActivateInputField(); // Setze den Fokus zurück auf das Input Field
            }
        });
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
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (inputField.isFocused)
            {
                CheckInput(inputField.text); // Überprüfe die Eingabe
                inputField.text = ""; // Setze das Input Field zurück
                inputField.ActivateInputField(); // Setze den Fokus zurück auf das Input Field
                inputField.Select(); // Wähle das Input Field aus, um sicherzustellen, dass der Cursor sichtbar bleibt
            }
        }

    }
    public void spawnEnemy()
    {
        float lowestPoint = transform.position.y - spawnOffset;
        float highestPoint = transform.position.y + spawnOffset;

        GameObject enemy = Instantiate(enemys, new Vector3(transform.position.x, Random.Range(lowestPoint, highestPoint), 0), Quaternion.identity);
        enemy.transform.up = -transform.right;


    }
    void CheckInput(string userInput)
    {
        // Finde alle Gegner im Spiel
        EnemyBehavoir[] enemies = FindObjectsOfType<EnemyBehavoir>();

        foreach (EnemyBehavoir enemy in enemies)
        {
            // Vergleiche den User-Input mit jedem Gegner-Wort
            if (userInput.Equals(enemy.textname.text, System.StringComparison.OrdinalIgnoreCase))
            {
                gun.GetComponent<GunScript>().Fire(enemy.transform.position);
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
