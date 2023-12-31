using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeControllerScript : WeaponController
{



    // Start is called before the first frame update
     protected override void Start()
    { base.Start(); }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedWeapon = Instantiate(weaponData.Prefab);
        spawnedWeapon.transform.position = transform.position;

        spawnedWeapon.GetComponent<KnifeBehaviour>().DirectionChecker(player.lastMovedVector);
    }
}
