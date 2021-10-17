using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private string gameOverText = "Game Over";
    private string victoryText = "Victory";
    [SerializeField] private int lives;
    [SerializeField] private int nbMissilesGain;
    private int missiles;
    [SerializeField] private int nbSpreadSecondsGain;
    private int nbSpreadSeconds;
    private float secondTimer = 1;
    private Text[] uiTexts;
    [SerializeField] private Canvas UI;
    private int nbAliens =0;
    private bool allSpawnerDestroyed = false;
    [SerializeField] private AudioSource CameraAudio;
    [SerializeField] private AudioSource PlayerAudio;
    [SerializeField] private AudioClip victory;

    private bool fadeOutMusic;
    private float fadeTime = 3;
    private float defaultCameraVolume;

    // Start is called before the first frame update
    void Start()
    {
        uiTexts = UI.GetComponentsInChildren<Text>();
        uiTexts[0].text = lives.ToString();
        uiTexts[3].enabled = false;
        uiTexts[4].enabled = false;
        defaultCameraVolume = CameraAudio.volume;

    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOutMusic)
        {
            CameraAudio.volume -= defaultCameraVolume * Time.deltaTime / fadeTime;
        }
        if (HasSpread())
        {
            secondTimer-=Time.deltaTime;
            if (secondTimer <= 0)
            {
                LoseSpreadSeconds();
                secondTimer = 1; //Pour qu'il le fasse à chaques secondes
            }
        }
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
    public void GainLife()
    {
        lives++;
        uiTexts[0].text = lives.ToString();
    }

    public bool HasMissile()
    {
        return missiles > 0;
    }

    public void LoseMissile()
    {
        missiles--;
        uiTexts[1].text = missiles.ToString();
    }

    public void GainMissile()
    {
        missiles+= nbMissilesGain;
        uiTexts[1].text = missiles.ToString();
    }

    public bool HasSpread()
    {
        return nbSpreadSeconds > 0;
    }

    public void LoseSpreadSeconds()
    {
        nbSpreadSeconds--;
        uiTexts[2].text = nbSpreadSeconds.ToString();
    }

    public void GainSpread()
    {
        nbSpreadSeconds += nbSpreadSecondsGain;
        uiTexts[2].text = nbSpreadSeconds.ToString();
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
            //Ici il y a une exception qui se lance. Pour résumé quand on ferme le jeu, il disable tout les alien et tout les spawner et cela call cette
            //fonction en boucle. Le truc c'est que les objet se font destroy aléatoirement, donc il arrive oû qu'il destroy le text et la caméra
            //avant de détruire les alien et les spawner. C'est pour ça que je vérifie si ils sont null
            if (uiTexts[4] != null && PlayerAudio != null)
            {
                uiTexts[4].text = victoryText;
                uiTexts[4].enabled = true;
                fadeOutMusic = true;
                PlayerAudio.PlayOneShot(victory);
            }
        }
    }
}
