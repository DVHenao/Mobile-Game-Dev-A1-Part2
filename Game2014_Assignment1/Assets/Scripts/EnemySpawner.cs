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

    private IEnumerator Spawner()
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

    private IEnumerator LootSpawner()
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
