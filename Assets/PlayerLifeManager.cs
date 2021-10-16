using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLifeManager : MonoBehaviour
{
    private float damageCooldown;
    [SerializeField] private float damageDelay = 0.5f;
    private AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        damageCooldown -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        print(other.gameObject.name);
        if (damageCooldown <= 0)
        {
            if (other.gameObject.tag == "Enemy")
            {
                damageCooldown = damageDelay;
                audioSource.Play();
            }
        }
    }
}
