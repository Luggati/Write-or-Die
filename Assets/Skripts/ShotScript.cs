using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{

    Vector3 shotDirection;
    int velocity = 70;
    int type;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position += shotDirection * velocity * Time.deltaTime;

        Vector3 pos = transform.position;
        if (pos.x < -28 || pos.x > 28 || pos.y < -15 || pos.y > 15)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (other.GetComponent<EnemyBehavoir>().GetEnemyType() == type)
            {
                GameObject.Find("LogicScript").GetComponent<LogicScript>().IncreaseScore(1);
                Destroy(other.gameObject);
            }
            Destroy(gameObject);
        }
    }

    public void SetShotDirection(Vector3 newShotDirection)
    {
        shotDirection = newShotDirection;
    }

    public void SetShotTyp(int shotType)
    {
        type = shotType;
        transform.GetChild(shotType).gameObject.SetActive(true);
    }

    public int GetShotTyp()
    {
        return type;
    }
}
