using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeManager : MonoBehaviour
{
    private float damageCooldown;
    [SerializeField] private float damageDelay = 0.5f;
    private AudioSource audioSource;
    private int jumpDecal= 2;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip dieSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponentInChildren<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        damageCooldown -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {   
        if (damageCooldown <= 0)
        {
            if (other.gameObject.tag == "Enemy"&& transform.position.y <= other.transform.position.y+ jumpDecal /*Pour savoir si il saute ou pas*/)
            {
                if (gameManager.isPlayerDead())
                {
                    //Détacher l'audio du space marine
                    audioSource.gameObject.transform.parent = null;
                    audioSource.PlayOneShot(dieSound);
                    gameObject.SetActive(false);
                }
                else
                {
                    damageCooldown = damageDelay;
                    audioSource.PlayOneShot(hurtSound);
                }
                gameManager.LoseLife();
            }
        }
    }
}
