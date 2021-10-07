using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private string fixedLifeText = "Vies: ";
    private string gameOverText = "Game Over";
    [SerializeField] int lives;
    [SerializeField] Text[] uiTexts = new Text[2];
    // Start is called before the first frame update
    void Start()
    {
        uiTexts[0].text = fixedLifeText + lives.ToString();
        uiTexts[1].text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoseLife()
    {
        lives--;
        uiTexts[0].text = fixedLifeText + lives.ToString();
        if (lives <= 0)
        {
            uiTexts[1].text = gameOverText;
        }
    }
}
