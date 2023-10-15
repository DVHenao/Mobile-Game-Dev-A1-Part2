using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called before the first frame update

    float spawnrate = 1f;

    GameObject[] enemyPrefabs;

    bool canSpawn = true; 
        



    void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnrate);

        while (canSpawn) {
            yield return wait;







            Instantiate(enemyPrefabs[0]);



            
        }
    }
}
