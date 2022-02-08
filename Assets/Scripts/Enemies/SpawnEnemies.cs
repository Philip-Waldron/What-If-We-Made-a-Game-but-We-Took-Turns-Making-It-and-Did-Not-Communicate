using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnEnemies : MonoBehaviour
{
    [Serializable]
    public struct Baddie
    {
        public GameObject Agent;
        public int SpawnChanceWeight;
    }

    public Baddie[] Enemies;
    [HideInInspector]
    public List<Enemy> SpawnedEnemies = new List<Enemy>();

    public float MinimumSpawnDistanceFromPlayer;
    public float MaximumSpawnDistanceFromPlayer;

    public float SpawnRate;
    private float _spawnTimer;
    private bool _canSpawn = true;

    private void Update()
    {
        UpdateSpawnTimer();

        if (_canSpawn)
        {
            Spawn();
        }
    }

    // Spawn an enemy at a random position within the valid spawn area.
    private void Spawn()
    {
        float distance = Random.Range(MinimumSpawnDistanceFromPlayer, MaximumSpawnDistanceFromPlayer);
        float angle = Random.Range(-Mathf.PI, Mathf.PI);

        Vector3 spawnPosition = PlayerController.Instance.transform.position ;
        spawnPosition += new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * distance;

        Enemy newEnemy = Instantiate(RollRandomEnemy(), spawnPosition, Quaternion.identity).GetComponent<Enemy>();
        SpawnedEnemies.Add(newEnemy);
        newEnemy.Spawner = this;

        _canSpawn = false;
    }

    // Roll for a random enemy to spawn based on the spawn weights.
    private GameObject RollRandomEnemy()
    {
        int totalWeight = 0;
        foreach (Baddie enemy in Enemies)
        {
            totalWeight += enemy.SpawnChanceWeight;
        }

        float rollForInitiative = Random.Range(0f, totalWeight);

        int evaluatingWeight = 0;
        for (int index = 0; index < Enemies.Length; index++)
        {
            evaluatingWeight += Enemies[index].SpawnChanceWeight;
            if (rollForInitiative <= evaluatingWeight)
            {
                return Enemies[index].Agent;
            }
        }

        Debug.LogError("We shouldn't have gotten here. If you see this message oh god oh fuck.");
        return null;
    }

    // Manage the internal clock for the movement rate.
    private void UpdateSpawnTimer()
    {
        if (_canSpawn)
        {
            _spawnTimer = 0;
        }

        if (!_canSpawn)
        {
            _spawnTimer += Time.deltaTime;
            if (_spawnTimer > SpawnRate)
            {
                _canSpawn = true;
                _spawnTimer -= SpawnRate;
            }
        }
    }

    // Max distance can't be smaller than Min.
    private void OnValidate()
    {
        if (MaximumSpawnDistanceFromPlayer < MinimumSpawnDistanceFromPlayer)
        {
            MaximumSpawnDistanceFromPlayer = MinimumSpawnDistanceFromPlayer;
        }
    }

    // Yummy gizmos to show spawn area.
    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.yellow;

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            UnityEditor.Handles.DrawWireDisc(GameObject.FindWithTag("Player").transform.position, Vector3.up, MinimumSpawnDistanceFromPlayer);
            UnityEditor.Handles.DrawWireDisc(GameObject.FindWithTag("Player").transform.position, Vector3.up, MaximumSpawnDistanceFromPlayer);
        }
    }
}
