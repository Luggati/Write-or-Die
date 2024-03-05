using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotScript : MonoBehaviour
{

    Vector3 shotDirection;
    int velocity = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += shotDirection * velocity * Time.deltaTime;
    }

    public void SetShotDirection(Vector3 newShotDirection)
    {
        shotDirection = newShotDirection;
    }
}
