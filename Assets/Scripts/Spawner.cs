using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnDisable()
    {
        transform.parent.gameObject.GetComponent<Alienspawner>().RemoveSpawner(gameObject);
    }
}
