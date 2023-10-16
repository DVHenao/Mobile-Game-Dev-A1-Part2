using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{


    public int damage = 10;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("Collsion ATTACKS ");

        if (collision.gameObject.tag == "Enemy")
        {

            Debug.Log("Collsion ATTACKS ENEMY");
            collision.GetComponent<EnemyScript>().health -= 10;
        }
    }
}
