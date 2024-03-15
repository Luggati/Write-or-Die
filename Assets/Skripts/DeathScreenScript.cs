using UnityEngine;
using UnityEngine.UI;

public class DeathScreenScript : MonoBehaviour
{

    public GameObject logicScript;
    public Text gameOvercurrentScoreTf;
    public Text scoreboardTf;
    // Start is called before the first frame update
    void Start()
    {
        scoreboardTf.text = logicScript.GetComponent<LogicScript>().GetScoreboardAsString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCurrentScore()
    {
        gameOvercurrentScoreTf.text = "Game Over \n Your Score: " + logicScript.GetComponent<LogicScript>().GetCurrentScore();
    }
}
