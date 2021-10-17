using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private string gameOverText = "Game Over";
    private string victoryText = "Victory";
    [SerializeField] private int lives;
    private Text[] uiTexts;
    [SerializeField] private Canvas UI;
    private int nbAliens =0;
    private bool allSpawnerDestroyed = false;
    [SerializeField] private GameObject Camera;
    [SerializeField] private AudioClip victory;

    // Start is called before the first frame update
    void Start()
    {
        uiTexts = UI.GetComponentsInChildren<Text>();
        uiTexts[0].text = lives.ToString();
        uiTexts[3].enabled = false;
        uiTexts[4].enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LoseLife()
    {

        if (isPlayerDead())
        {
            uiTexts[3].text = gameOverText;
            uiTexts[3].enabled = true;
        }
        else
        {
            lives--;
            uiTexts[0].text = lives.ToString();
        }
    }

    public bool isPlayerDead()
    {
        return lives <= 0;
    }

    public void NewAlien()
    {
        ++nbAliens;
    }
    public void AlienKilled()
    {
        print(nbAliens);
        --nbAliens;
        CheckIfVictory();
    }
    public void AllSpawnersDestroyed()
    {
        allSpawnerDestroyed = true;
        CheckIfVictory();
    }

    private void CheckIfVictory()
    {
        if(allSpawnerDestroyed && nbAliens == 0)
        {
            uiTexts[4].text = victoryText;
            uiTexts[4].enabled = true;
            Camera.GetComponent<AudioSource>().Stop();
            Camera.GetComponent<AudioSource>().PlayOneShot(victory);
        }
    }
}
