using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCaster : MonoBehaviour
{
    //create ann array of every hit gameobject
    public List<GameObject> currentHitObjects = new List<GameObject>();
    public Transform[] enemyPositions;
    public float currentHitDistance;

    public float sphereRadius;
    public float maxDistance;
    public LayerMask layerMask;

    public Vector3 origin;
    public Vector3 direction;

    // Update is called once per frame
    void Update()
    {
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
            
        }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(origin, origin + direction * currentHitDistance);
        Gizmos.DrawWireSphere(origin + direction * currentHitDistance, sphereRadius);
    }
}
