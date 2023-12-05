/*
EnemyScript.cs
Made by Emmanuelle Henao, Student Number: 101237746
Last Modified: October 14th, 2023
Game2014 - Mobile Dev
Revision History: Enemy chases player, inflicts damage and can take hits - Oct 14th, 2023 
*/

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    public GameObject player;
    public float distance;
    public GameObject UI;


    Animator animator;
    SpriteRenderer spriteRenderer;
    float lastHorizontalVector;
    float lastVerticalVector;

    float currentMoveSpeed;
    float currentHealth;
    float currentDamage;
    int currentExperienceValue;


    private void Awake()
    {
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
        currentExperienceValue = enemyData.ExperienceValue;
    }

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player");
        UI = GameObject.Find("UICanvas");
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        if (direction.x != 0)
            lastHorizontalVector = direction.x;
        if (direction.y != 0)
            lastVerticalVector = direction.y;


        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, currentMoveSpeed * Time.deltaTime);

        // everything above this comment is for homing towards player and setting correct sprite direction


        if (direction != Vector2.zero)// for animation purposes
        {
            //animator.SetBool("isMoving", true);
            SpriteDirectionChecker();
        }
        else
        {
            //animator.SetBool("isMoving", false);
        }

        
    }


    void SpriteDirectionChecker()// animation flip checker
    {
        if (lastHorizontalVector < 0) { spriteRenderer.flipX = true; }
        else { spriteRenderer.flipX = false; }
    }

    public void OnCollisionEnter2D(Collision2D collision) // to inflict damage on the player
    {
        if (collision.gameObject.tag == "Player")
        {
            player.GetComponent<PlayerScript>().TakeDamage(currentDamage);
        }

    }
    public void Die()
    {
        UI.GetComponent<MainMenuUI>().MoveScore();
        Debug.Log("enemy Died");
        player.GetComponent<PlayerScript>().IncreaseExperience(currentExperienceValue);

        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        es.OnEnemyKilled();

        Destroy(gameObject);
    }
    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        
        if (currentHealth <= 0)
        {
            Die();
        }
    }

}

