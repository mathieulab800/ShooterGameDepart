using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    [SerializeField] private GameObject ammoPrefab;
    [SerializeField] private int maxAmmos;
    [SerializeField] private int ammoSpeed;
    [SerializeField] private Transform ammoSpawn;
    private GameObject[] ammos;

    private float shootCountdown = 0;
    private float shootdelay = 0.1f;

    // Start is called before the first frame update
    void Start() 
    {
        ammos = new GameObject[maxAmmos];
        for (int i = 0; i < ammos.Length; i++)
        {
            GameObject ammo = Instantiate(ammoPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            ammos.SetValue(ammo, i);
            ammos[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        shootCountdown -= Time.deltaTime;
        if (Input.GetButton("Fire1")&& shootCountdown<=0)
        {
            shootCountdown = shootdelay;
            GameObject ammoToShoot = null;
            bool ammoSelected = false;
            //Get first inactive ammo
            foreach(GameObject ammo in ammos)
            {
                if(!ammo.activeSelf && !ammoSelected)
                {
                    ammoToShoot = ammo;
                }
            }
            if(ammoToShoot != null)
            {
                ammoToShoot.SetActive(true);
                Rigidbody rigidBody = ammoToShoot.GetComponent<Rigidbody>();
                rigidBody.velocity = Vector3.zero;
                ammoToShoot.transform.position = ammoSpawn.position;
                Vector3 rotation = transform.rotation.eulerAngles;
                ammoToShoot.transform.rotation = Quaternion.Euler(rotation.x, 0, rotation.z);
                rigidBody.AddForce(transform.forward * ammoSpeed, ForceMode.Impulse);
                ammoToShoot.GetComponent<AudioSource>().Play();
            }
        }
    }
}
