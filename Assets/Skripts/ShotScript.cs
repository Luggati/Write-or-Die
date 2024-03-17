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
        int actVel = velocity + (2 - type) * 10;
        transform.position += shotDirection * actVel * Time.deltaTime;

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
            Destroy(gameObject);
            int enemyType = other.GetComponent<EnemyBehavoir>().GetEnemyType();
            if (enemyType > 2 || enemyType == type)
            {
                other.GetComponent<EnemyBehavoir>().Destroy();
            }
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
