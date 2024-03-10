using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public GameObject logicScript;
    public float rotationSpeed = 30.0f;
    Quaternion targetRotation;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.gameObject.CompareTag("Enemy"))
        {
            logicScript.GetComponent<LogicScript>().DecreaseHealth();
        }
    }

    public void SetTargetAngle(Vector3 direction)
    {
        float zielwinkel = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90;
        targetRotation = Quaternion.Euler(0, 0, zielwinkel);
    }
}
