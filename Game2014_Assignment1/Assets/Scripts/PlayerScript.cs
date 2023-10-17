using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public float moveSpeed;
  
    Rigidbody2D rb;
    Vector2 moveDir;

    Animator animator;
    SpriteRenderer spriteRenderer;
    float lastHorizontalVector;
    float lastVerticalVector;

    public float health;



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

        attackAreaRight = transform.GetChild(0).gameObject;
        attackAreaLeft = transform.GetChild(1).gameObject;
        health = 100;


        rb.velocity = new Vector2(0, 0);
        animator.SetBool("isMoving", false);
        moveDir = new Vector2 (0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        InputManagement();
        if (moveDir != Vector2.zero){
            animator.SetBool("isMoving", true); 
            SpriteDirectionChecker();
        }
        else {
            animator.SetBool("isMoving", false);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Attack();
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
      
            float moveX = Input.GetAxisRaw("Horizontal");
            float moveY = Input.GetAxisRaw("Vertical");

            moveDir = new Vector2(moveX, moveY).normalized;

        if (moveDir.x != 0)
        {
            lastHorizontalVector = moveDir.x;
        }
        if (moveDir.y != 0)
        {
            lastVerticalVector = moveDir.y;
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
    public void TakeDamage()
    {
        health -= 10;

        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("player died!");

    }
    
    public void Attack()
    {

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
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PickUp")
        {
            Destroy(collision.gameObject);
            StartCoroutine(PickUp());
        }
   
    }

    private IEnumerator PickUp()
    {
        WaitForSeconds wait = new WaitForSeconds(5);

        moveSpeed *= 2;

        yield return wait;

        moveSpeed /= 2;
    }

}
