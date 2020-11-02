using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPower : MonoBehaviour
{
    public PlayerController playerController;
    public bool specialActive = false;

    [SerializeField] float _specialDuration = 10;

    public GameObject weaponSpecial;
    public GameObject reticles;
    public bool specialUsed = false;
    public bool isAiming = false;

    void Update()
    {
        if (specialActive == true)
        {
            Aim();

            //if input is clicked and special is active, fire
            if (Input.GetKeyDown(KeyCode.Mouse0) && specialActive == true)
            {
                Debug.Log("Missiles Launched!");
                Fire();

                //deactivate special if fired
                weaponSpecial.SetActive(false);
                specialActive = false;
                specialUsed = true;
            }

        }


    }

    public IEnumerator SpecialSequence()
    {
        specialActive = true;

        weaponSpecial.SetActive(true);

        //isAiming = true;

        yield return new WaitForSeconds(_specialDuration);

        weaponSpecial.SetActive(false);

        //isAiming = false;

        specialActive = false;

        specialUsed = true;
    }
    

    public void Aim()
    {
        //set ui reticles as active
        Debug.Log("Aiming...");
        //reticles.SetActive(true);
    }

    public void Fire()
    {
         //activate missile behavior
         //Debug.Log("Firing Missiles!");
    }

}
