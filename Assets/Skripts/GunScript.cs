using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject player;
    public GameObject shot;
    AudioSource[] shotAudio;
    bool lastWeapon = true;
    int weaponType = 0;

    // Start is called before the first frame update
    void Start()
    {
        shotAudio = gameObject.GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Fire(EnemyBehavoir enemy)
    {
        shotAudio[weaponType].Play();
        float enemyVelocity = enemy.movespeed;
        Vector3 enemyRealPos = enemy.GetComponent<Collider2D>().transform.position;
        Vector3 enemyPos = enemyRealPos - new Vector3(enemyVelocity/5,0,0);  // enemyPos wird vorhergesagt
        Vector3 direction = Vector3.Normalize(enemyPos - transform.position);  // Richtung des Schusses
        
        GameObject newShot = Instantiate(shot) as GameObject;
        
        TriggerShotAnimation();

        newShot.transform.position = transform.position;    // Schuss wird an der Gun des Spielers erstellt
        newShot.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);  // Drehe den Schuss in Schussrichtung

        newShot.GetComponent<ShotScript>().SetShotDirection(direction);     // Setze die Schussrichtung
        newShot.GetComponent<ShotScript>().SetShotTyp(weaponType);    // Setze den Schusstyp

        player.GetComponent<PlayerScript>().SetTargetAngle(direction);  // Setze die Zielrichtung des Spielers
    }

    private void TriggerShotAnimation()
    {
        if (lastWeapon)
        {
            player.GetComponentInChildren<Animator>().SetTrigger("Shot1");  // Starte die Schussanimation
        }
        else
        {
            player.GetComponentInChildren<Animator>().SetTrigger("Shot2");  // Starte die Schussanimation
        }
        lastWeapon = !lastWeapon;
    }

    public void SetWeaponType(int type)
    {
        weaponType = type;
    }

}
