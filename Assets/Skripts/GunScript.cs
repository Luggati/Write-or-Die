using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{

    public GameObject shot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire(Vector3 enemyPos)
    {
        Vector3 direction = Vector3.Normalize(enemyPos - transform.position);
        GameObject newShot = Instantiate(shot) as GameObject;
        Debug.Log("Fire");
        newShot.transform.position = transform.position;
        newShot.transform.Rotate(direction);
        newShot.GetComponent<ShotScript>().SetShotDirection(direction);
    }

    //Key Listener
    void OnGUI()
    {
        if (Event.current.Equals(Event.KeyboardEvent("f")))
        {
            Fire(new Vector3(10, 2, 0));
        }
    }
}