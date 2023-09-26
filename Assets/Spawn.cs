using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] Transform[] spawnPositions;
    [SerializeField] GameObject[] enemiesToSpawn;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0, .5f);
    }

    void SpawnEnemy()
    {
        Instantiate(enemiesToSpawn[1], spawnPositions[Random.Range(0, 7)].position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
