using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    [Header("Weapon Stats")]
    public WeaponScriptableObject weaponData;
    float currentCooldown;


    protected PlayerScript player;



    // Start is called before the first frame update
    protected virtual void Start()
    {

        player = FindObjectOfType<PlayerScript>();
        currentCooldown = weaponData.cooldownDuration;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;

        if (currentCooldown <= 0f)
        {
            Attack();
        }
    }

    protected virtual void Attack()
    {
        currentCooldown = weaponData.cooldownDuration;
    }
}
