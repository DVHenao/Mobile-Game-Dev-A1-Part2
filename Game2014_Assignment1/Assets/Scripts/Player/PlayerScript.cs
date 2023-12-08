/*
PlayerScript.cs
Made by Emmanuelle Henao, Student Number: 101237746
Last Modified: October 14th, 2023
Game2014 - Mobile Dev
Revision History: Cerntal script for all game logic and player logic. seperate into game manager and play manager later - Oct 14th, 2023 
*/



using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    public PlayerScriptableObject playerData;


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

    public GameObject UIObject;

    public bool attacking;
    public GameObject attackAreaRight;
    public GameObject attackAreaLeft;
    public float attackSpeed = 0.25f;
    public float timer;

    public float currentHealth;
    public float currentRecovery;
    public float currentMoveSpeed;
    public float currentMight;
    public float currentProjectileSpeed;

    public int experience = 0;
    public int level = 1;
    public float experienceCap = 100;

    public float iFrameDuration;
    float iFrameTimer;
    bool iFrameActive;


    InventoryManager inventory;
    public int weaponIndex;
    public int passiveItemIndex;

    public GameObject firstpit, secondpit;
    //public GameObject secondWeaponTest;

    public GameObject gameManager;

    [Header("UI")]
    public Image healthBar;
    public Image expBar;
    public TMP_Text LevelText;

    public void Awake()
    {
        inventory = GetComponent<InventoryManager>();

        currentHealth = playerData.MaxHealth;
        currentRecovery = playerData.Recovery;
        currentMoveSpeed = playerData.MoveSpeed;
        currentMight = playerData.Might;
        currentProjectileSpeed = playerData.ProjectileSpeed;

        //SpawnPassiveItem(firstpit);
        //SpawnPassiveItem(secondpit);
    }


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        UIObject = GameObject.Find("UICanvas");

        attackAreaRight = transform.GetChild(0).gameObject;
        attackAreaLeft = transform.GetChild(1).gameObject;


        rb.velocity = new Vector2(0, 0);
        animator.SetBool("isMoving", false);
        moveDir = new Vector2 (0, 0);
        lastMovedVector = new Vector2(1, 0);

        ResumeGame(); // this is here just in case
        UpdateHealthBar();
        UpdateEXPBar();
        UpdateLevelText();
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

        if(iFrameTimer>0)
        {
            iFrameTimer -= Time.deltaTime;
        }
        else if (iFrameActive)
            iFrameActive = false;

        //RecoverHealth(currentRecovery);
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
        rb.velocity = new Vector2(moveDir.x * currentMoveSpeed, moveDir.y * currentMoveSpeed);
    }


    void SpriteDirectionChecker()
    {
        if (lastHorizontalVector < 0) { spriteRenderer.flipX = true; }
        else { spriteRenderer.flipX = false; }
    }
    public void TakeDamage(float dmg) // self explantory
    {
        if(!iFrameActive)
        {
            audioSource.clip = takeDamageSound;
            audioSource.Play();

            currentHealth -= dmg;
            UIObject.GetComponent<MainMenuUI>().TakeDamage();

            iFrameTimer = iFrameDuration;
            iFrameActive = true;

            if (currentHealth <= 0)
            {
                Die();
            }
            UpdateHealthBar();
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

    public void UpdateEXPBar()
    {
        expBar.fillAmount = (float)experience / experienceCap;
    }

    public void UpdateLevelText()
    {
        LevelText.text = "LVL: " + level.ToString();
    }


    public void IncreaseExperience(int amount)
    {
        experience += amount;
        UpdateEXPBar();
        LevelUpChecker();
    }

    public void LevelUpChecker()
    {
        if (experience >= experienceCap)
        {
            level++;
            experience -= Convert.ToInt32(experienceCap);
            experienceCap = experienceCap * 1.08f;

            UpdateLevelText();


            gameManager.GetComponent<GameManager>().StartLevelUp();
        } 
    }

    public void RecoverHealth(float recovery)
    {
        if (currentHealth < playerData.MaxHealth)
        {
            currentHealth += recovery;

            if (currentHealth > playerData.MaxHealth)
            {
                currentHealth = playerData.MaxHealth;
            }
        }
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHealth/playerData.MaxHealth;
    }


    public void SetStartingPlayer(PlayerScriptableObject PlayerData)
    {
        playerData = PlayerData;
    }

    public void SpawnWeapon(GameObject weapon)
    {
        Debug.Log("SpawnWeapon()");
        if ((weaponIndex >= inventory.weaponSlots.Count - 1))
        {
            Debug.LogError("Inventory slots already full");
            return;
        }

        GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
        spawnedWeapon.transform.position += new Vector3(0, -0.75f, 0);

        spawnedWeapon.transform.SetParent(transform);
        inventory.AddWeapon(weaponIndex, spawnedWeapon.GetComponent<WeaponController>());

        weaponIndex++;
    }

    public void SpawnPassiveItem(GameObject passiveItem)
    {
        Debug.Log("SpawnPassiveItem()");
        if ((passiveItemIndex >= inventory.passiveItemSlots.Count - 1))
        {
            Debug.LogError("Inventory slots already full");
            return;
        }

        GameObject spawnedPassiveItem = Instantiate(passiveItem, transform.position, Quaternion.identity);
        spawnedPassiveItem.transform.position += new Vector3(0, -0.75f, 0);

        spawnedPassiveItem.transform.SetParent(transform);
        inventory.AddPassiveItem(passiveItemIndex, spawnedPassiveItem.GetComponent<PassiveItem>());

        passiveItemIndex++;
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

       currentMoveSpeed += 3;//replace with currenty speed later

        yield return wait;

        currentMoveSpeed -= 3;//replace with curernt speed later 
    }

}
