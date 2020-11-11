using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPower : MonoBehaviour
{
    public PlayerController playerController;
    public bool specialActive = false;

    [SerializeField] float _specialDuration = 10;

    public GameObject weaponSpecial;
    public GameObject missilePrefab;
    [SerializeField] Transform spawnPosition;
    public float speed = 5;
    public GameObject reticles;
    public bool specialUsed = false;
    public bool isAiming = false;

    //create ann array of every hit gameobject
    public List<GameObject> currentHitObjects = new List<GameObject>();
    //public Transform[] enemyPositions;
    public float currentHitDistance;

    public float sphereRadius;
    public float maxDistance;
    public LayerMask layerMask;

    public Vector3 origin;
    public Vector3 direction;

    void Update()
    {
        if (specialActive == true)
        {
            Aim();

            origin = this.transform.position;
            direction = transform.forward;

            //clears out the list every update
            currentHitObjects.Clear();

            RaycastHit[] hits = Physics.SphereCastAll(origin, sphereRadius, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
            //for each successful raycast hit, add the gameobject to the current list of hits
            foreach (RaycastHit hit in hits)
            {
                currentHitObjects.Add(hit.transform.gameObject);
                //currentHitDistance = hit.distance;            

                if (hit.transform.tag == "Enemy")
                {
                    //Debug.Log("Target Aquired!");
                    //save enemy positions
                    //update positions of enemy location reticles

                }
            }

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
            if (specialUsed == true)
            {
                reticles.SetActive(false);
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + direction * currentHitDistance);
        Gizmos.DrawWireSphere(origin + direction * currentHitDistance, sphereRadius);
    }

    public void Aim()
    {
        //set ui reticles as active
        //Debug.Log("Aiming...");
        reticles.SetActive(true);
    }

    public void Fire()
    {
        //instantiate missiles
        //Debug.Log("Firing Missiles!");
        GameObject missile = Instantiate(missilePrefab);

        missile.transform.position = spawnPosition.position;

        Vector3 rotation = missile.transform.rotation.eulerAngles;

        missile.transform.rotation = Quaternion.Euler(rotation.x, transform.eulerAngles.y, rotation.z);

        missile.GetComponent<Rigidbody>().AddForce(transform.forward * speed, ForceMode.Impulse);
    }

}
