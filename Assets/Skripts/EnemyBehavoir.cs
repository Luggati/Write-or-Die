using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.UI;

public class EnemyBehavoir : MonoBehaviour
{
    public float movespeed = 5;
    public Text textname;
    int type;
    float speedRangePercent = 0.15f;
    float startSpeed;
    Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        SetRandomType();
        SetRandomName(Random.Range(1,4) + Random.Range(1,4));
        movespeed += Random.Range(-movespeed * speedRangePercent, movespeed * speedRangePercent);
        startSpeed = movespeed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (direction * movespeed) * Time.deltaTime;
    }

    public void SetRandomName(int wordLenght)
    {
        LogicScript ls = GameObject.Find("LogicScript").GetComponent<LogicScript>();
        UtilsScript utils = GameObject.Find("Utils").GetComponent<UtilsScript>();
        if (utils == null) Debug.Log("Utils is null");
        if (ls.GetLanguage().Equals("en"))
        {
            textname.text = utils.GetRandomEngWordWithLenght(wordLenght).ToLower();
        }
        else
        {
            textname.text = utils.GetRandomGerWordWithLenght(wordLenght).ToLower();
        }
    }

    void SetRandomType()
    {
        type = Random.Range(0, 3);
        transform.GetChild(type).gameObject.SetActive(true);
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

}
