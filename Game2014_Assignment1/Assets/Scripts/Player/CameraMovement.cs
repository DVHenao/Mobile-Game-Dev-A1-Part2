/*
CameraMovement.cs
Made by Emmanuelle Henao, Student Number: 101237746
Last Modified: October 14th, 2023
Game2014 - Mobile Dev
Revision History: part 2 added character movement and animation - Oct 14th, 2023 
*/



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform target;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = target.position + offset; // self explanatory
    }
}
