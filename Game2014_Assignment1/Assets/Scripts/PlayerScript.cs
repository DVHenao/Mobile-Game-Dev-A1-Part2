using System.Collections;
using System.Collections.Generic;
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




    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            lastHorizontalVector = moveDir.x;
        if (moveDir.y != 0)
            lastVerticalVector = moveDir.y;

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



}
