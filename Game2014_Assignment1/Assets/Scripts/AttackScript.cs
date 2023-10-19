/*
AttackScript.cs
Made by Emmanuelle Henao, Student Number: 101237746
Last Modified: October 14th, 2023
Game2014 - Mobile Dev
Revision History: attacking functionality implemented here - Oct 14th, 2023 
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{


    public int damage = 10;

    private void OnTriggerEnter2D(Collider2D collision)// self explanatory
    {

        if (collision.gameObject.tag == "Enemy")
        {

            collision.GetComponent<EnemyScript>().health -= 10;// self explanatory
        }
    }
}
