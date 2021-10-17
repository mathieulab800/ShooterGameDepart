using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Alien : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject target;
    private NavMeshAgent navMeshAgent;
    private bool spawning;
    private float fallingSpeed =0.1f;

    private void OnEnable()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        spawning = true;
        if (navMeshAgent != null)
            navMeshAgent.enabled = false;
    }

    private void OnDisable()
    {
        gameManager.AlienKilled();
        if (navMeshAgent != null)
            navMeshAgent.enabled = false;
    }

     private bool IsGrounded(){
       return Physics.Raycast(transform.position, Vector3.down, 0.1f);
     }

    void Update()
    {
        if (navMeshAgent != null&&!spawning)
        {
            navMeshAgent.destination = target.transform.position;
        }
    }

    private void FixedUpdate()
    {
        if (spawning)
        {
            transform.position += Vector3.down * fallingSpeed;
            if (IsGrounded())
            {
                spawning = false;
                if (navMeshAgent != null)
                    navMeshAgent.enabled = true;
            }
        }
    }

    public void setTarget(GameObject target)
    {
        this.target = target;
    }

    public void setGameManager(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }
}
