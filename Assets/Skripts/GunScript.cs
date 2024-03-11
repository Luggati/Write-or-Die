using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject player;
    public GameObject shot;
    AudioSource shotAudio;

    // Start is called before the first frame update
    void Start()
    {
        shotAudio = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire(EnemyBehavoir enemy, int shotType)
    {
        float enemyVelocity = enemy.movespeed;
        Vector3 enemyPos = enemy.transform.position - new Vector3(enemyVelocity/5,0,0);  // enemyPos wird vorhergesagt
        Vector3 direction = Vector3.Normalize(enemyPos - transform.position);  // Richtung des Schusses
        
        GameObject newShot = Instantiate(shot) as GameObject;
        shotAudio.Play();

        newShot.transform.position = transform.position;    // Schuss wird an der Position des Spielers erstellt
        newShot.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);  // Drehe den Schuss in Schussrichtung

        newShot.GetComponent<ShotScript>().SetShotDirection(direction);     // Setze die Schussrichtung
        newShot.GetComponent<ShotScript>().SetShotTyp(shotType);    // Setze den Schusstyp

        player.GetComponent<PlayerScript>().SetTargetAngle(direction);  // Setze die Zielrichtung des Spielers
    }

}
