using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Alien : MonoBehaviour
{

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
}
