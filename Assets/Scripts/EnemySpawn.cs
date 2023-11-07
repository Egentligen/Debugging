using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Spawn: MonoBehaviour
{
    [Header("Enemies")]
    [SerializeField] GameObject[] enemiesToSpawn;
    [SerializeField, Tooltip("Precentages")] float[] spawnChances;
    [SerializeField] Vector2[] spawnPositions;
    [Header("Spawning")]
    [SerializeField] float spawnRate;


    void Start()
    {
        StartCoroutine(EnemySpawnRoutine());
    }

    void SpawnEnemy()
    {
        int randomSpawnPos = Random.Range(0, spawnPositions.Length - 1);

        Instantiate(enemiesToSpawn[GetRandomEnemy()], spawnPositions[randomSpawnPos], Quaternion.identity);
    }

    private int GetRandomEnemy()
    {
        float random = Random.Range(0f, 1f);
        float adding = 0f;
        float total = 0f;

        for (int i = 0; i < spawnChances.Length; i++)
        {
            total += spawnChances[i];
        }
        for (int i = 0; i < enemiesToSpawn.Length; i++)
        {
            if (spawnChances[i] / total + adding >= random)
            {
                return i;
            }
            else
            {
                adding += spawnChances[i] / total;
            }
        }
        return 0;
    }

    IEnumerator EnemySpawnRoutine() 
    {
        yield return new WaitForSeconds(spawnRate);

        SpawnEnemy();

        StartCoroutine(EnemySpawnRoutine());
    }
}
