using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeCollisionManager : MonoBehaviour
{ 
    [SerializeField] private string[] oneShotTags;
    [SerializeField] private string[] loseLifeTags;
    [SerializeField] private int nbLife;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach(string tag in loseLifeTags)
        {
            if (other.gameObject.tag == tag)
            {
                --nbLife;
                if (nbLife <= 0)
                {
                    gameObject.SetActive(false);
                }
            }
        }
        foreach (string tag in oneShotTags)
        {
            if (other.gameObject.tag == tag)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
