using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    [SerializeField] private int damage;
    private List<LifeCollisionManager> targets = new List<LifeCollisionManager>();

    //Ici on abonne un énemi à l'explosion ou non.
    private void OnTriggerEnter(Collider other)
    {
        LifeCollisionManager collision = other.gameObject.GetComponent<LifeCollisionManager>();
        if(collision != null)
        {
            targets.Add(collision);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        LifeCollisionManager collision = other.gameObject.GetComponent<LifeCollisionManager>();
        if (targets.Contains(collision))
        {
            targets.Remove(collision);
        }
    }

    private void OnDisable()
    {
        foreach(LifeCollisionManager target in targets)
        {
            target.DealDamage(damage);
        }
    }

}
