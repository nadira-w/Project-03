﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;

    public SpecialPower specialPower;

    //[Header movement]
    public float speed = 8f;
    public float sprintSpeed = 16f;
    public float gravity = -10f;
    public float jumpHeight = 3f;

    public void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        //if player is alive and button is pressed
            
            if (Input.GetKeyDown(KeyCode.Space) && specialPower.specialUsed == false)
            {
                StartCoroutine(specialPower.SpecialSequence());   
            }
            
    }
}