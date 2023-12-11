using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave
    {
        public string waveName;

        public List<EnemyGroup> enemyGroups;
        public int waveQuota; //the total number of enemies to spawn in this wave
        public float spawnInterval; //the interval at which to spawn enemies
        public int spawnCount; // the number of enemies already spawned in this wave
    }

    [System.Serializable]
    public class EnemyGroup
    {
        public string enemyName;
        public int enemyCount; // # of enemies to spawn
        public int spawnCount; // number of enemies already spawned
        public GameObject enemyPrefab;
    }

    public List<Wave> waves;
    public int currentWaveCount; // the index of the current wave, list starts from 0

    GameObject player;

    [Header("Spawner Attributes")]
    float spawnTimer;
    public int enemiesAlive;
    public int maxEnemiesAllowed;
    public bool maxEnemiesReached = false; 
    public float waveInterval; // the interval between each wave
    public bool isWaveActive = false;

    [Header("Spawn Positions ")]
    public List<Transform> relativeSpawnPoints;



    void Start()
    {
        player = GameObject.FindWithTag("Player");
        CalculateWaveQuota();
    }

    void Update()
    {
        if(currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0 && !isWaveActive) // check if the wave has ended and the next wave should start
        {
            StartCoroutine(BeginNextWave());
        }


        spawnTimer += Time.deltaTime;

        if(spawnTimer >= waves[currentWaveCount].spawnInterval) 
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }
    }

    IEnumerator BeginNextWave()
    {

        isWaveActive = true;

        // wave for 'waveinterval' seconds before starting the next wave
        yield return new WaitForSeconds(waveInterval);

        //if there are more waves to start after the current wave, move on to the next wave.
        if (currentWaveCount < waves.Count - 1)
        {
            isWaveActive = false;
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }


    void CalculateWaveQuota()
    {
        int currentWaveQuota = 0;

        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }
        waves[currentWaveCount].waveQuota = currentWaveQuota;
        Debug.LogWarning(currentWaveQuota);
    }

    void SpawnEnemies()
    {
        //check if min number of enemies has been spawned
        if (waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached)
        {
            //spawn each type of enemy until the quota is filled
            foreach(var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                //check in min number of enemies of this type has spawned
                if(enemyGroup.spawnCount < enemyGroup.enemyCount)
                {

                    Vector2 spawnPosition = new Vector2(player.transform.position.x + Random.Range(-10f, 10f),
                                                        player.transform.position.y + Random.Range(-10f, 10f));

                    Instantiate(enemyGroup.enemyPrefab, spawnPosition, Quaternion.identity);

                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;


                    if (enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return;
                    }

                }
            }
        }

    }

    public void OnEnemyKilled()
    {
        enemiesAlive--;

        if (enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
        }
    }



















    /*
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
    */
}
