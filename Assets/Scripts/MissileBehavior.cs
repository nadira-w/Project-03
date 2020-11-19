using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehavior : MonoBehaviour

{
    public float speed = 20;
    public float turnRate = 60;

    public Transform target;

    public Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Update()
    {
        Vector3 direction = target.position - transform.position;
        direction = direction.normalized;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(direction, transform.up), turnRate);

        rb.velocity = transform.up * speed;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
    
}
