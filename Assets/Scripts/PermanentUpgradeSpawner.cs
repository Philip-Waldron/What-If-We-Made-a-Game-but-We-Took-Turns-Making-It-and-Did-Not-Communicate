using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PermanentUpgradeSpawner : MonoBehaviour
{
    public float UpgradeSpawnRate = 5.0f;
    private float timer = 0.0f;
    public GameObject[] upgrades;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= UpgradeSpawnRate)
        {
            SpawnUpgrade();
            timer = 0.0f;
        }
    }

    void SpawnUpgrade()
    {
        if (upgrades.Length <= 0)
        {
            Debug.Log("I can't spawn an upgrade if there's no upgrades! >:(");
            return;
        }

        GameObject upgrade = upgrades[Random.Range(0, upgrades.Length - 1)];

        //whenever this game comes out with a new map update the bounds of this calculation pls :c
        Vector3 spawnPoint = new Vector3(Random.Range(-15, 15), 0.5f, Random.Range(-15, 15));

        Instantiate(upgrade, spawnPoint, Quaternion.identity);
    }
}
