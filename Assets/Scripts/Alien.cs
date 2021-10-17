using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Alien : MonoBehaviour
{
    [SerializeField] private UnityEvent death;
    [SerializeField] private GameObject target;
    private NavMeshAgent navMeshAgent;
    private bool spawning;
    private float fallingSpeed =0.05f;

    private void OnEnable()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        spawning = true;
        if (navMeshAgent != null)
            navMeshAgent.enabled = false;
    }

    private void OnDisable()
    {
        death.Invoke();
        if (navMeshAgent != null)
            navMeshAgent.enabled = false;
    }

     private bool IsGrounded(){
       return Physics.Raycast(transform.position, Vector3.down, 0.1f);
     }

    void Update()
    {
        if (spawning)
        {
            transform.position += Vector3.down* fallingSpeed;
            if (IsGrounded())
            {
                spawning = false;
                if (navMeshAgent != null)
                    navMeshAgent.enabled = true;
            }
        }
        else if (navMeshAgent != null)
        {
            navMeshAgent.destination = target.transform.position;
        }
    }

    public void setTarget(GameObject target)
    {
        this.target = target;
    }

    public void AddDeathListener(UnityAction action)
    {
        //Le souci, c'est qu'en ajoutant un listener, IL APPELLE LA FONCTION.
        death.AddListener(action);
    }
}
