using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private Vector3 offest = new Vector3(0f, 20f, -25f);
    void LateUpdate()
    {
        transform.position = player.transform.position + offest;
    }
}
