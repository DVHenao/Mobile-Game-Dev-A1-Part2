using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RighteousFireController : WeaponController
{
    // Start is called before the first frame update
   protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedWeapon = Instantiate(prefab);

        spawnedWeapon.transform.position = transform.position;
        spawnedWeapon.transform.parent = transform;//test
    }
}
