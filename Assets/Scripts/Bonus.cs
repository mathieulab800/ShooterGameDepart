using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField] private int rotationSpeed= 2;
    private AudioSource audioSource;
    // Start is called before the first frame update
    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        audioSource.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, rotationSpeed, 0);
    }
}
