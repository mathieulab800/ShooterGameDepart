using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    [SerializeField] private GameObject healthBonusPrefab;
    [SerializeField] private int maxHealthBonus;
    private GameObject[] healthBonus;

    [SerializeField] private GameObject missileBonusPrefab;
    [SerializeField] private int maxMissileBonus;
    private GameObject[] missileBonus;

    [SerializeField] private GameObject spreadBonusPrefab;
    [SerializeField] private int maxSpreadBonus;
    private GameObject[] spreadBonus;

    // Start is called before the first frame update
    void Awake()
    {
        healthBonus = new GameObject[maxHealthBonus];
        for (int i = 0; i < healthBonus.Length; i++)
        {
            GameObject bonus = Instantiate(healthBonusPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            healthBonus.SetValue(bonus, i);
        }

        missileBonus = new GameObject[maxMissileBonus];
        for (int i = 0; i < missileBonus.Length; i++)
        {
            GameObject bonus = Instantiate(missileBonusPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            missileBonus.SetValue(bonus, i);
        }

        spreadBonus = new GameObject[maxSpreadBonus];
        for (int i = 0; i < spreadBonus.Length; i++)
        {
            GameObject bonus = Instantiate(spreadBonusPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            spreadBonus.SetValue(bonus, i);
        }
    }

    public void SpawnBonus(Vector3 location, BonusTypes type)
    {
        GameObject[] bonusList = BonusClassingType(type);
        foreach(GameObject bonus in bonusList)
        {
            if (bonus != null) //J'explique pourquoi je vérifie s'il est null dans le game manager
            {
                if (!bonus.activeSelf)
                {
                    bonus.SetActive(true);
                    bonus.transform.position = location;
                    break;
                }
            }
        }
    }

    private GameObject[] BonusClassingType(BonusTypes type)
    {
        GameObject[] bonus = new GameObject[0];
        switch (type)
        {
            case BonusTypes.HEALTH:
                bonus = healthBonus;
                break;
            case BonusTypes.MISSILE:
                bonus = missileBonus;
                break;
            case BonusTypes.SPREAD:
                bonus = spreadBonus;
                break;
        }
        return bonus;
    }
}
