using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alienspawner : MonoBehaviour
{
    [SerializeField] private GameObject alienPrefab;
    [SerializeField] private int maxAliens;
    [SerializeField] private int aliensLimit;
    [SerializeField] private GameObject player;
    private GameObject[] aliens;
    private List<GameObject> spawners;

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
            aliens.SetValue(alien, i);
            aliens[i].GetComponent<Alien>().setTarget(player);
            aliens[i].SetActive(false);
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
    }

    private List<GameObject> getAllChildren()
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in transform)
            children.Add(child.gameObject);
        return children;
    }
}
