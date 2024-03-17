using UnityEngine;
using UnityEngine.UI;

public class EnemyBehavoir : MonoBehaviour
{
    public float movespeed = 4.5f;
    public Text textname;
    int type;
    float speedRangePercent = 0.05f;
    float startSpeed;
    Vector3 direction;
    Color[] colors = { Color.yellow, Color.cyan, Color.red, Color.green, Color.magenta };
    AudioSource[] deathsounds;

    // Start is called before the first frame update
    void Start()
    {
        SetRandomType();
        int wordMinlenght = (type + 1) * 2;
        SetRandomName(Random.Range(wordMinlenght, wordMinlenght + 2));
        deathsounds = GetComponents<AudioSource>();
        movespeed += Random.Range(-movespeed * speedRangePercent, movespeed * speedRangePercent);
        startSpeed = movespeed;

        
    }

    // Update is called once per frame
    void Update()
    {
        float actVel = movespeed + (2 -type) / 2;
        transform.position = transform.position + (direction * actVel) * Time.deltaTime;
    }

    public void SetRandomName(int wordLenght)
    {
        LogicScript ls = GameObject.Find("LogicScript").GetComponent<LogicScript>();
        UtilsScript utils = GameObject.Find("Utils").GetComponent<UtilsScript>();
        if (ls.GetLanguage().Equals("en"))
        {
            textname.text = "" + utils.GetRandomEngWordWithLenght(wordLenght).ToLower();
        }
        else
        {
            textname.text = "" + utils.GetRandomGerWordWithLenght(wordLenght).ToLower();
        }
    }

    void SetRandomType()
    {
        int roll = Random.Range(0, 100);
        if (roll > 8)
        {
            type = Random.Range(0, 3);
        }
        else
        {
            type = Random.Range(3, 5);
        }
        
        transform.GetChild(type).gameObject.SetActive(true);
        textname.color = colors[type];
    }

    public void SetEnemyDirection(Vector3 newDirection)
    {
        direction = newDirection;
    }
  
    public int GetEnemyType()
    {
        return type;
    }

    public string GetEnmyText()
    {
        return textname.text;
    }

    public void ResetSpeed()
    {
        movespeed = startSpeed;
    }

    public void SetTextColor(Color color)
    {
        textname.color = color;
    }

    public void Destroy()
    {
        if (type <= 2)
        {
            deathsounds[type].Play();
            gameObject.GetComponent<Collider2D>().enabled = false;
            transform.GetChild(type).gameObject.GetComponentInChildren<Animator>().Play("Destroy");
            GameObject.Find("LogicScript").GetComponent<LogicScript>().IncreaseScore(1);
        }
        else if (type == 3)
        {
            GameObject.Find("LogicScript").GetComponent<LogicScript>().IncreaseHealth(1);
            DeleteEnemy();
        }
        else if (type == 4)
        {
            EnemyBehavoir[] enemies = FindObjectsOfType<EnemyBehavoir>();
            foreach (EnemyBehavoir enemy in enemies)
            {
                if (enemy.GetEnemyType() <= 2)
                {
                    enemy.Destroy();
                }
            }
            DeleteEnemy();
        }
        
        
    }

    public void DeleteEnemy()
    {
        Destroy(gameObject);
    }

}
