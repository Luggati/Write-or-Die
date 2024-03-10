using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{
    Renderer[] graphics;

    Vector3 shotDirection;
    int velocity = 70;

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
            GameObject.Find("EnemyHandler").GetComponent<EnemyHandler>().Hit(other.gameObject);
            Destroy(gameObject);
        }
    }

    public void SetShotDirection(Vector3 newShotDirection)
    {
        shotDirection = newShotDirection;
    }

    public void SetShotTyp(string type)
    {
        graphics = GetComponentsInChildren<Renderer>();
        if (type.Equals("L"))
        {
            graphics[0].enabled = true;
        }
        else if (type.Equals("R"))
        {
            graphics[1].enabled = true;
        }
        else if (type.Equals("M"))
        {
            graphics[2].enabled = true;
        }

    }
}
