using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Alienspawner : MonoBehaviour
{
    [SerializeField] private GameObject alienPrefab;
    [SerializeField] private int maxAliens;
    [SerializeField] private int aliensLimit;
    [SerializeField] private GameObject player;
    private GameObject[] aliens;
    private List<GameObject> spawners;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private BonusManager bonusManager;

    private float spawnTimer = 0;
    [SerializeField] private float spawnDelay = 5f;

    // Start is called before the first frame update
    void Start()
    {
        spawners = getAllChildren();
        aliens = new GameObject[maxAliens];
        for (int i = 0; i < aliens.Length; i++)
        {
            GameObject alien = Instantiate(alienPrefab, new Vector3(0, 0, 0), Quaternion.identity);

            //Initialize alien
            Alien alienScript = alien.GetComponent<Alien>();
            BonusSpawner bonusSpawner = alien.GetComponent<BonusSpawner>();
            alienScript.setTarget(player);
            alienScript.setGameManager(gameManager);
            bonusSpawner.SetBonusManager(bonusManager);
            alien.SetActive(false);

            aliens.SetValue(alien, i);
            gameManager.NewAlien();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (aliensLimit > 0)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer > spawnDelay)
            {
                spawnTimer = 0;
                if (spawners.Count > 0)
                {
                    GameObject spawner;
                    do
                    {
                        spawner = spawners[Random.Range(0, spawners.Count)];
                    } while (!spawner.activeSelf);

                    GameObject alien = getNextAlien();
                    if (alien != null)
                    {
                        gameManager.NewAlien();
                        aliensLimit--;
                        alien.SetActive(true);
                        alien.transform.position = spawner.transform.position;
                    }
                }
                
            }
        }
    }

    private GameObject getNextAlien()
    {
        foreach (GameObject alien in aliens)
        {
            if (!alien.activeSelf)
            {
                return alien;
            }
        }
        return null;
    }

    public void RemoveSpawner(GameObject spawner)
    {
        spawners.Remove(spawner);
        if (spawners.Count == 0)
        {
            gameManager.AllSpawnersDestroyed();
        }
    }

    private List<GameObject> getAllChildren()
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in transform)
            children.Add(child.gameObject);
        return children;
    }
}
