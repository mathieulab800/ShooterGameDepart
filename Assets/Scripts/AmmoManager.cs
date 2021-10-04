using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{

    [SerializeField] GameObject[] ammos = new GameObject[1];
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
                ammo.transform.position = new Vector3(transform.position.x + 5, transform.position.y + 5, transform.position.z + 5);
                Vector3 rotation = ammo.transform.rotation.eulerAngles;
                ammo.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);
                rigidBody.AddForce(transform.forward * 30, ForceMode.Impulse);
            }
        }
    }
}
