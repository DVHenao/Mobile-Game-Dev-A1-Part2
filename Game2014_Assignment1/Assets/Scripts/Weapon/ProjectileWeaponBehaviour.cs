  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileWeaponBehaviour : MonoBehaviour
{
    public WeaponScriptableObject weaponData;

    protected Vector3 direction;
    public float destroyAfterSeconds;




    // Start is called before the first frame update
    protected virtual void Start()
    {
        Destroy(gameObject, destroyAfterSeconds);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DirectionChecker(Vector3 dir) 
    {

        Debug.Log(dir.x + ", " + dir.y + ", " + dir.z);
        direction = dir;

       float dirX = direction.x;
       float dirY = direction.y;

        //Vector3 scale = transform.localScale;
        //Vector3 rotation = transform.rotation.eulerAngles;

        if (dirX > 0 && dirY == 0)//right
        {
            transform.rotation = Quaternion.Euler(0, 0, -45);
        }
        if (dirX < 0 && dirY == 0)//left
        {
            transform.rotation = Quaternion.Euler(0, 0, 135);
        }
        else if (dirX == 0 && dirY < 0)//down
        {
            transform.rotation = Quaternion.Euler(0, 0, -135);
        }
        else if (dirX == 0 && dirY > 0)//up
        {
            transform.rotation = Quaternion.Euler(0, 0, 45);
        }

        else if (dir.x > 0 && dir.y > 0) //up right
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (dir.x > 0 && dir.y < 0) //right down
        {
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else if (dir.x < 0 && dir.y < 0) //down left
        {
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (dir.x < 0 && dir.y > 0) //left up
        {
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (dirX == 0 && dirY == 0)
        {
            
        }






        //transform.localScale = scale;
        //transform.rotation = Quaternion.Euler(rotation);


    }
}
