using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusSpawner : MonoBehaviour
{
    private BonusManager bonusManager;
    [SerializeField] private float healthSpawnRate;
    [SerializeField] private float missileSpawnRate;
    [SerializeField] private float spreadSpawnRate;
    [SerializeField] private float noneSpawnRate;

    public void SetBonusManager(BonusManager bonusManager)
    {

        this.bonusManager = bonusManager;
    }

    private void OnDisable()
    {
        switch (Random.Range(0, 101))
        {
            case int n when (n >= 0 && n < healthSpawnRate):
                bonusManager.SpawnBonus(transform.position, BonusTypes.HEALTH);
                break;
            case int n when (n >= healthSpawnRate && n < healthSpawnRate+ missileSpawnRate):
                bonusManager.SpawnBonus(transform.position, BonusTypes.MISSILE);
                break;
            case int n when (n >= healthSpawnRate + missileSpawnRate && n < healthSpawnRate + missileSpawnRate + spreadSpawnRate):
                bonusManager.SpawnBonus(transform.position, BonusTypes.SPREAD);
                break;
        }
    }
    //Au lieu de travailler en pourcentage, on va l'auto rescale comme ça pas d'erreur
    private void rescaleSpawnRate()
    {
        float total = healthSpawnRate + missileSpawnRate + spreadSpawnRate + noneSpawnRate;
        healthSpawnRate = healthSpawnRate * 100 / total;
        missileSpawnRate = missileSpawnRate * 100 / total;
        spreadSpawnRate = spreadSpawnRate * 100 / total;
        //On s'en fout de none il sera auto géré dans le switch
        /*noneSpawnRate = noneSpawnRate * 100 / total; */
    }
}
