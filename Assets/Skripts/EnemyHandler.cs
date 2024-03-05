using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class EnemyHandler : MonoBehaviour
{
    public GameObject enemys;
    public float spawnrate = 2;
    private float timer = 0;
    public float heightoffset = 3; 
    // Start is called before the first frame update
    void Start()
    {
        spawnEnemy();  
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnrate )
        {
            timer = timer + Time.deltaTime;
        }else
        {
            spawnEnemy();
            timer = 0;
        }
       
    }
    public void spawnEnemy()
    {
        float lowestPoint = transform.position.y - heightoffset;
        float highestPoint = transform.position.y + heightoffset;
        Instantiate(enemys, new Vector3(transform.position.x,Random.Range(lowestPoint,highestPoint),0), transform.rotation);
    }
}
