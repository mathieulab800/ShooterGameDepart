using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoCollisionManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name != "SpaceMarine")
        {
            gameObject.SetActive(false);
        }
    }

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "SpaceMarine")
        {
            gameObject.SetActive(false);
        }
    }*/
}
