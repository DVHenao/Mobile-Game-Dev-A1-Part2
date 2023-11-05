/*
EnemySpawner.cs
Made by Emmanuelle Henao, Student Number: 101237746
Last Modified: October 14th, 2023
Game2014 - Mobile Dev
Revision History: Simple Spawner that handles both enemyspawns and loot spawns - Oct 14th, 2023 
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public float enemySpawnRate = 1f;

    public float lootSpawnRate = 5f;

    public GameObject lootPrefab;

    public GameObject[] enemyPrefabs;

    public bool canSpawn = true; 
        



    void Start()
    {
        StartCoroutine(Spawner());
        StartCoroutine(LootSpawner());
    }

    private IEnumerator Spawner() //simple spawner for enemies
    {
        WaitForSeconds wait = new WaitForSeconds(enemySpawnRate);

        while (canSpawn) {
            yield return wait;

            int rand = Random.Range(0, enemyPrefabs.Length - 1);
            GameObject enemyToSpawn = enemyPrefabs[rand];
            
            Vector2 randPosition = new Vector2(Random.Range(-12,12),Random.Range(-12,12));


            Instantiate(enemyToSpawn, randPosition, Quaternion.identity);

            enemySpawnRate -= 0.01f;
            
        }
    }

    private IEnumerator LootSpawner() //simple spawner for pickups
    {
        WaitForSeconds wait = new WaitForSeconds(lootSpawnRate);

        while (canSpawn)
        {
            yield return wait;

            Vector2 randPosition = new Vector2(Random.Range(-12, 12), Random.Range(-12, 12));


            Instantiate(lootPrefab, randPosition, Quaternion.identity);
        }
    }

}
