using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController controller;

    public SpecialPower specialPower;

    [Header("Movement")]
    public float speed = 8f;

    [SerializeField] Camera cameraController;
    public AudioClip _special;

    public void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        //if player is alive and button is pressed

        if (Input.GetKeyDown(KeyCode.Space) && specialPower.specialUsed == false)
        {
            AudioHelper.PlayClip2D(_special, 1);

            StartCoroutine(specialPower.SpecialSequence());   
        }
    }
}