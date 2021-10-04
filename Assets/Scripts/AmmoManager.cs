using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{

    [SerializeField] GameObject[] ammos = new GameObject[1];
    [SerializeField] Transform ammoSpawn;
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject ammo in ammos)
        {
            ammo.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            foreach(GameObject ammo in ammos)
            {
                ammo.SetActive(true);
                Rigidbody rigidBody = ammo.GetComponent<Rigidbody>();
                rigidBody.velocity = Vector3.zero;
                ammo.transform.position = ammoSpawn.position;
                Vector3 rotation = transform.rotation.eulerAngles;
                ammo.transform.rotation = Quaternion.Euler(rotation.x, 0, rotation.z);
                rigidBody.AddForce(transform.forward * 30, ForceMode.Impulse);
            }
        }
    }
}
