using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RighteousFireBehaviour : MeleeWeaponBehaviour
{

    List<GameObject> markedEnemies;

    protected override void Start()
    {
        base.Start();
        markedEnemies = new List<GameObject>();
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !markedEnemies.Contains(collision.gameObject))
        {
            Debug.Log("rf overlap");
            EnemyScript enemy = collision.GetComponent<EnemyScript>();
            enemy.TakeDamage(GetCurrentDamage());
            markedEnemies.Add(collision.gameObject);
            //ReducePierce();

        }
    }


}

