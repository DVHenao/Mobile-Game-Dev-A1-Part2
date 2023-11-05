/*
PlayerScript.cs
Made by Emmanuelle Henao, Student Number: 101237746
Last Modified: October 14th, 2023
Game2014 - Mobile Dev
Revision History: Cerntal script for all game logic and player logic. seperate into game manager and play manager later - Oct 14th, 2023 
*/



using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public float moveSpeed;
  
    Rigidbody2D rb;
    public Vector2 moveDir;


    public AudioSource audioSource;
    public AudioClip pickupSound;
    public AudioClip swingSound;
    public AudioClip takeDamageSound;

    public FixedJoystick joystick;

    Animator animator;
    SpriteRenderer spriteRenderer;
    public float lastHorizontalVector;
    public float lastVerticalVector;
    public Vector2 lastMovedVector;



    public float health;
    public GameObject UIObject;


    public bool attacking;
    public GameObject attackAreaRight;
    public GameObject attackAreaLeft;
    public float attackSpeed = 0.25f;
    public float timer;




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        UIObject = GameObject.Find("UICanvas");

        attackAreaRight = transform.GetChild(0).gameObject;
        attackAreaLeft = transform.GetChild(1).gameObject;
        health = 30;


        rb.velocity = new Vector2(0, 0);
        animator.SetBool("isMoving", false);
        moveDir = new Vector2 (0, 0);
        lastMovedVector = new Vector2(1, 0);

        ResumeGame(); // this is here just in case
    }

    // Update is called once per frame
    void Update()
    {
        InputManagement(); //movement

        if (moveDir != Vector2.zero){ // animation stuff here
            animator.SetBool("isMoving", true); 
            SpriteDirectionChecker();
        }
        else {
            animator.SetBool("isMoving", false);
        }


        if (Input.GetKeyDown(KeyCode.Space) || Input.touchCount > 1) // both touch and keyboard controls for debugging
        {
            if(!attacking)
            Attack();
        }

        if (Input.touchCount > 0) 
        {
            if (Input.GetTouch(0).position.x > Screen.width/2)
            {
                if (!attacking)
                    Attack();
            }

            if (Input.GetTouch(1).position.x > Screen.width / 2)
            {
                if (!attacking)
                    Attack();
            }

        }


        if (attacking)
        {
            timer += Time.deltaTime;

            if (timer >= attackSpeed)
            {
                timer = 0;
                attacking = false;
                animator.SetBool("isAttacking", attacking);
                attackAreaRight.SetActive(attacking);
                attackAreaLeft.SetActive(attacking);
            }
        }
    }

    void FixedUpdate()
    {
        Move();
    }


    void InputManagement()
    {
      //keyboard input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY).normalized;

        // mobile/joystick input
        if (Input.touchCount > 0)
        moveDir = new Vector2(joystick.Horizontal,joystick.Vertical);

        if (moveDir.x != 0)
        {
            lastHorizontalVector = moveDir.x;
            lastMovedVector = new Vector2(lastHorizontalVector, 0.0f); // for attacking directino when not moving
        }
        if (moveDir.y != 0)
        {
            lastVerticalVector = moveDir.y;
            lastMovedVector = new Vector2(0.0f, lastVerticalVector); // for attacking directino when not moving
        }
        if (moveDir.y != 0 && moveDir.x != 0)
        {
            lastMovedVector = new Vector2(lastHorizontalVector, lastVerticalVector); // for attacking directino when not moving
        }

    }


    void Move()
    {
        rb.velocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
    }


    void SpriteDirectionChecker()
    {
        if (lastHorizontalVector < 0) { spriteRenderer.flipX = true; }
        else { spriteRenderer.flipX = false; }
    }
    public void TakeDamage() // self explantory
    {

        audioSource.clip = takeDamageSound;
        audioSource.Play();

        health -= 10;
        UIObject.GetComponent<MainMenuUI>().TakeDamage();

        if (health <= 0)
        {
            Die();
        }
    }

    void PauseGame() // self explantory
    {
        Time.timeScale = 0;
    }
    void ResumeGame() // self explantory
    {
        Time.timeScale = 1;
    }
    public void Die() // self explantory
    {
        PauseGame();
        Debug.Log("player died!");
    }
    
    public void Attack() // sets the attack collider to active, which is later deactivated in Update()
    {
        audioSource.clip = swingSound;
        audioSource.Play();
        attacking = true;

        if (lastHorizontalVector < 0)
        {
            attackAreaLeft.SetActive(attacking);
        }
        if (lastHorizontalVector > 0)
        {
            attackAreaRight.SetActive(attacking);
        }
        animator.SetBool("isAttacking", attacking);
    }
    public void OnTriggerEnter2D(Collider2D collision) // Pickup Audio
    {
        if (collision.gameObject.tag == "PickUp")
        {
            audioSource.clip = pickupSound;
            audioSource.Play();

            Destroy(collision.gameObject);
            StartCoroutine(PickUp());
        }
   
    }

    private IEnumerator PickUp() //move speed bonus from pick up
    {
        WaitForSeconds wait = new WaitForSeconds(5);

        moveSpeed += 3;

        yield return wait;

        moveSpeed -= 3;
    }

}
