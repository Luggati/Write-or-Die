using UnityEngine;
using UnityEngine.UI;

public class EnemyBehavoir : MonoBehaviour
{
    public float movespeed = 4.5f;
    public Text textname;
    int type;
    float speedRangePercent = 0.05f;
    float startSpeed;
    bool hasArrived = false;
    bool isNatural = true;
    int difficulty = 0;

    Vector3 direction;
    Vector3 destination;
    Color[] colors = { Color.yellow, Color.cyan, Color.red, Color.green, Color.magenta, Color.white };
    AudioSource[] deathsounds;

    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        SetRandomType();
        int wordMinlenght = (type + 1) * 2;
        SetRandomName(Random.Range(wordMinlenght, wordMinlenght + 2));
        deathsounds = GetComponents<AudioSource>();
        movespeed += Random.Range(-movespeed * speedRangePercent, movespeed * speedRangePercent);
        startSpeed = movespeed;

        if (type == 5)
        {
            destination = transform.position - new Vector3(Random.Range(5,30),0,0);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        float actVel = movespeed + (2 - type) / 2;
        if (type == 5)
        {
            if (!hasArrived)
            {
                direction = destination - transform.position;
                if (direction.magnitude < 1f)
                {
                    // TODO: play build-animation
                    GameObject.Find("EnemyHandler").GetComponent<EnemyHandler>().AddActiveSpawner(transform.position);
                    hasArrived = true;
                }
                else
                {
                    // TODO: destination in position.Translate einbinden
                    transform.position = transform.position + (direction.normalized * actVel) * Time.deltaTime;
                }
            } 
            else
            {
                // TODO: spawn Starbase (im enemyhandler die position dieses objektes hinzufügen. dort dann für jede postion in liste einen "normalen" enemy spawnwn lassen. 
            }
        }
        else
        {
            transform.position = transform.position + (direction * actVel) * Time.deltaTime;
        }
    }

    public void SetRandomName(int wordLenght)
    {
        LogicScript ls = GameObject.Find("LogicScript").GetComponent<LogicScript>();
        UtilsScript utils = GameObject.Find("Utils").GetComponent<UtilsScript>();
        if (ls.GetLanguage().Equals("en"))
        {
            textname.text = utils.GetRandomEngWordWithLenght(wordLenght).ToLower();
        }
        else
        {
            textname.text = utils.GetRandomGerWordWithLenght(wordLenght).ToLower();
        }
    }

    void Initialize()
    {
        GameObject eh = GameObject.Find("EnemyHandler");
        if (transform.position.x < eh.GetComponent<Transform>().position.x)
        {
            isNatural = false;
        }

        difficulty = eh.GetComponent<EnemyHandler>().GetDifficulty();
    }

    void SetRandomType()
    {
        if (isNatural)
        {
            int roll = Random.Range(0, 100);
            if (roll > 8)
            {
                type = Random.Range(0, 3);
            }
            else if (roll > 3)
            {
                if (difficulty > 2)
                {
                    type = 5;
                }
                else
                {
                    type = Random.Range(0, 3);
                }
            }
            else
            {
                type = Random.Range(3, 5);
            }
        }
        else
        {
            type = Random.Range(0, 2);
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
        LogicScript ls = GameObject.Find("LogicScript").GetComponent<LogicScript>();
        if (type <= 2)
        {
            deathsounds[type].Play();
            gameObject.GetComponent<Collider2D>().enabled = false;
            transform.GetChild(type).gameObject.GetComponentInChildren<Animator>().Play("Destroy");
            ls.IncreaseScore(1);
        }
        else if (type == 3)
        {
            ls.IncreaseHealth(1);
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
        } else if (type == 5)
        {
            GameObject.Find("EnemyHandler").GetComponent<EnemyHandler>().RemoveActiveSpawner(transform.position);
            ls.AddHexcores(1);
            DeleteEnemy();
        }
        
        
    }

    public void DeleteEnemy()
    {
        Destroy(gameObject);
    }

}
