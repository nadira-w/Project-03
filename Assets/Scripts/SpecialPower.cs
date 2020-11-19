using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialPower : MonoBehaviour
{
    public PlayerController playerController;

    [Header("Player Special")]
    private bool specialActive = false;
    [SerializeField] float _specialDuration = 10;
    public GameObject weaponSpecial;
    public GameObject reticles;
    public bool specialUsed = false;
    public bool isAiming = false;

    [Header("Missiles")]
    public MissileBehavior missilePrefab;
    public Rigidbody _rb;
    public Transform spawnPosition;
    public Vector3 origin;
    public Vector3 direction;

    [Header("Enemy Tracking")]
    public Transform target;
    public float currentHitDistance;

    public float sphereRadius;
    public float maxDistance;
    public LayerMask layerMask;

    void start()
    {
        _rb = missilePrefab.GetComponent<Rigidbody>();
    }
    void Update()
    {
        if (specialActive == true)
        {
            Aim();

            origin = playerController.transform.position;
            direction = transform.forward;

            RaycastHit[] hits = Physics.SphereCastAll(origin, sphereRadius, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
            //for each successful raycast hit, get the transforms of the targets
            foreach (RaycastHit hit in hits)
            {
                //currentHitObjects.Add(hit.transform);
                target = hit.transform;
                currentHitDistance = hit.distance;

                //if input is clicked and special is active, fire
                if (Input.GetKeyDown(KeyCode.Mouse0) && specialActive == true)
                {
                    //Debug.Log("Missiles Launched!");
                    Fire();

                    //deactivate special if fired
                    weaponSpecial.SetActive(false);
                    specialActive = false;
                    specialUsed = true;
                }

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

        yield return new WaitForSeconds(_specialDuration);

        weaponSpecial.SetActive(false);

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
        //for each enemy targeted, instantiate missiles
        //Debug.Log("Firing Missiles!");
        MissileBehavior newMissile = Instantiate(missilePrefab, spawnPosition.position, spawnPosition.rotation);

        newMissile.target = target;

    }

}
