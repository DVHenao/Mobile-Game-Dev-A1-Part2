using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed;

    public float distance;


    Animator animator;
    SpriteRenderer spriteRenderer;
    float lastHorizontalVector;
    float lastVerticalVector;
    


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        player = GameObject.FindWithTag("Player");

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


        transform.position = Vector2.MoveTowards(this.transform.position, player.transform.position, moveSpeed * Time.deltaTime);




        if (direction != Vector2.zero)
        {
            //animator.SetBool("isMoving", true);
            SpriteDirectionChecker();
        }
        else
        {
            //animator.SetBool("isMoving", false);
        }
    }


    void SpriteDirectionChecker()
    {
        if (lastHorizontalVector < 0) { spriteRenderer.flipX = true; }
        else { spriteRenderer.flipX = false; }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision Detected");

        if (collision == null)
        { Debug.Log("collision is null"); }
        else
        {
            if (collision.gameObject.tag == "Player")
            {
                player.GetComponent<PlayerScript>().TakeDamage();
            }

            gameObject.transform.position = new Vector2(Random.Range(-13,13), Random.Range(-13, 13)     );
        }
    }


}

