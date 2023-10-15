using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public float spawnrate = 1f;

    public GameObject[] enemyPrefabs;

    public bool canSpawn = true; 
        



    void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnrate);

        while (canSpawn) {
            yield return wait;

            int rand = Random.Range(0, enemyPrefabs.Length - 1);
            GameObject enemyToSpawn = enemyPrefabs[rand];
            
            Vector2 randPosition = new Vector2(Random.Range(-13,13),Random.Range(-13,13));


            Instantiate(enemyToSpawn, randPosition, Quaternion.identity);
            
        }
    }
}
