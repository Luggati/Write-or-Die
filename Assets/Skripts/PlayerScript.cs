using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public GameObject logicScript;
    public float rotationSpeed = 50.0f;
    Quaternion targetRotation;
    Quaternion orgiginalRotation;
    public Animator playerWeaponAnimator;
    public Animator playerEngineAnimator;

    // Start is called before the first frame update
    void Start()
    {
        orgiginalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void SetTargetAngle(Vector3 direction)
    {
        float zielwinkel = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -90;
        targetRotation = Quaternion.Euler(0, 0, zielwinkel);
    }

    public void KillPlayer()
    {
        playerWeaponAnimator.Play("Explosion");
        playerEngineAnimator.Play("End");
        
        //ship.enabled = false;
    }

    public void ResetPlayer()
    {
        transform.rotation = orgiginalRotation;
        targetRotation = orgiginalRotation;
        playerWeaponAnimator.GetComponent<Renderer>().enabled = true;
        playerWeaponAnimator.Play("Idle");
        playerEngineAnimator.Play("Engine");
        
        //ship.enabled = true;
    }
}
