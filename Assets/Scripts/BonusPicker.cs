using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusPicker : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private GameManager gameManager;
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponentInChildren<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "HealthBonus")
        {
            gameManager.GainLife();
            audioSource.PlayOneShot(pickupSound);
        }
        if (other.gameObject.tag == "MissileBonus")
        {
            gameManager.GainMissile();
            audioSource.PlayOneShot(pickupSound);
        }
        if (other.gameObject.tag == "SpreadBonus")
        {
            gameManager.GainSpread();
            audioSource.PlayOneShot(pickupSound);
        }
    }
}
