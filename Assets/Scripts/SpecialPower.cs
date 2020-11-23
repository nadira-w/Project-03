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
    public Gauge gauge;
    public GameObject meter;

    [Header("Missiles")]
    public MissileBehavior missilePrefab;

    public GameObject missile;
    public float numShots = 7f;
    public float spreadAngle = 4f;
    public float timeBetweenShots = .5f;

    private float nextShot = 0f;

    public Rigidbody _rb;
    public Transform spawnPosition;
    public Vector3 origin;
    public Vector3 direction;
    [SerializeField] AudioClip _missileFire;

    [Header("Enemy Tracking")]
    public List<Transform> targets = new List<Transform>();
    public float currentHitDistance;
    public float sphereRadius;
    public float maxDistance;
    public LayerMask layerMask;

    void Start()
    {
        _rb = missilePrefab.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (specialActive == true)
        {
            Aim();
            
            origin = playerController.transform.position;
            Vector3 playerDirection = playerController.transform.forward;
            direction = playerDirection;

            targets.Clear();

            RaycastHit[] hits = Physics.SphereCastAll(origin, sphereRadius, direction, maxDistance, layerMask, QueryTriggerInteraction.UseGlobal);
            //for each successful raycast hit, get the transforms of the targets

                foreach (RaycastHit hit in hits)
                {
                targets.Add(hit.transform);

                //target = hit.transform;

                currentHitDistance = hit.distance;

                //if input is clicked and special is active, fire
                if (Input.GetKeyDown(KeyCode.Mouse0) && specialActive == true)
                {
                    //Debug.Log("Missiles Launched!");

                    Fire();

                    //AudioHelper.PlayClip2D(_missileFire, 1);

                    //deactivate special if fired
                    weaponSpecial.SetActive(false);
                    specialActive = false;
                    specialUsed = true;
                    meter.SetActive(false);
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

        reticles.SetActive(false);
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
        meter.SetActive(true);
        gauge.coolingDown = true;
    }

    public void Fire()
    {
        //for each enemy targeted, instantiate missiles
        //Debug.Log("Firing Missiles!");
        nextShot = Time.time + timeBetweenShots;
        var qAngle = Quaternion.AngleAxis(-numShots / 2f * spreadAngle, transform.up) * transform.rotation;
        var qDelta = Quaternion.AngleAxis(spreadAngle, transform.up);

        for (var i = 0; i < numShots; i++)
        {
            MissileBehavior newMissile = Instantiate(missilePrefab, spawnPosition.position, qAngle);

            //target = target;
            for (var f = 0; f < targets.Count; f++)
            {
                newMissile.target = targets[f];
            }
        }
    }

}
