using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoManager : MonoBehaviour
{
    [SerializeField] private GameObject ammoPrefab;
    [SerializeField] private int maxAmmos;
    [SerializeField] private int ammoSpeed;

    [SerializeField] private GameObject missilePrefab;
    [SerializeField] private int maxMissile;
    [SerializeField] private int missileSpeed;
    private GameObject[] missiles;


    [SerializeField] private Transform ammoSpawn;
    [SerializeField] private GameManager gameManager;
    private GameObject[] ammos;

    private float shootCountdown = 0;
    private float shootdelay = 0.1f;
    private float missileCountdown = 0;
    private float missiledelay = 1f;
    [SerializeField] private AudioClip spreadShot;

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

        missiles = new GameObject[maxMissile];
        for (int i = 0; i < missiles.Length; i++)
        {
            GameObject missile = Instantiate(missilePrefab, new Vector3(0, 0, 0), Quaternion.identity);
            missiles.SetValue(missile, i);
            missiles[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        shootCountdown -= Time.deltaTime;
        missileCountdown -= Time.deltaTime;
        if (Input.GetAxis("Fire1") > 0.5 && shootCountdown<=0)
        {
            shootCountdown = shootdelay;
            if (gameManager.HasSpread())
            {
                Vector3[] spreadDirections = { transform.forward * 0.5f + transform.right * 0.5f,
                                            transform.forward,
                                            transform.forward * 0.5f + transform.right * -0.5f};
                foreach(Vector3 rotation in spreadDirections)
                {
                    GameObject ammoToShoot = GetNextAmmo();
                    if (ammoToShoot != null)
                    {
                        ShootProjectile(ammoToShoot, rotation);
                        ammoToShoot.GetComponent<AudioSource>().PlayOneShot(spreadShot);
                    }
                }
                
            }
            else
            {
                GameObject ammoToShoot = GetNextAmmo();
                if (ammoToShoot != null)
                {
                    Vector3 rotation = transform.forward;
                    ShootProjectile(ammoToShoot, rotation);
                    ammoToShoot.GetComponent<AudioSource>().Play();
                }
            }
        } else if (Input.GetAxis("Fire2") > 0.5 && missileCountdown <= 0)
        {
            if (gameManager.HasMissile())
            {
                missileCountdown = missiledelay;
                gameManager.LoseMissile();
                GameObject missileToShoot = GetNextMissile();
                if (missileToShoot!=null)
                {
                    Vector3 rotation = transform.forward;
                    ShootProjectile(missileToShoot, rotation);
                    missileToShoot.GetComponent<AudioSource>().Play();
                }
            }
        }
    }

    private void ShootProjectile(GameObject projectile, Vector3 direction)
    {
        projectile.SetActive(true);
        Rigidbody rigidBody = projectile.GetComponent<Rigidbody>();
        rigidBody.velocity = Vector3.zero;
        projectile.transform.position = ammoSpawn.position;
        projectile.transform.rotation = Quaternion.Euler(direction.x, 0, direction.z);
        rigidBody.AddForce(direction* ammoSpeed, ForceMode.Impulse);
    }

    private GameObject GetNextAmmo()
    {
        foreach (GameObject ammo in ammos)
        {
            if (!ammo.activeSelf)
            {
                return ammo;
            }
        }
        return null;
    }

    private GameObject GetNextMissile()
    {
        foreach (GameObject missile in missiles)
        {
            if (!missile.activeSelf)
            {
                return missile;
            }
        }
        return null;
    }
}
