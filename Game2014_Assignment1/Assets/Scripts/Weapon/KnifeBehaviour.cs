using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBehaviour : ProjectileWeaponBehaviour
{

    KnifeControllerScript kcs;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        kcs = FindObjectOfType<KnifeControllerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * kcs.speed * Time.deltaTime; //test
    }
}
