using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBehavior : MonoBehaviour

{
    public Collider c;
    [SerializeField] public float speed = 2f;
    [SerializeField] public float turnRate = 5f;
    [SerializeField] public Transform target;
    public List<Transform> targets = new List<Transform>();
    [SerializeField] public float force = 5f;
    [SerializeField] private float timeBeforeHoming;

    public Rigidbody rb;
    private Transform missile;
    private bool shouldHome;

    public GameObject explosionPrefab;
    [SerializeField] AudioClip _missileFly;
    [SerializeField] AudioClip _missileStrike;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        missile = GetComponent<Transform>();

        StartCoroutine(WaitBeforeHoming());

        AudioHelper.PlayClip2D(_missileFly, 1);
    }

    public void FixedUpdate()
    {
        if (shouldHome)
        {
            rb.velocity = transform.forward * speed;

            var targetRot = Quaternion.LookRotation(target.transform.position - missile.position);

            rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, targetRot, turnRate));
        }

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            //trigger particle explosion
            Instantiate(explosionPrefab, missile.position, missile.rotation);

            //play explosion audio
            AudioHelper.PlayClip2D(_missileStrike, 1);

            //then deactivate game object
            Destroy(gameObject);
        }
    }

    private IEnumerator WaitBeforeHoming()
    {
        rb.AddForce(Vector3.up * force, ForceMode.Impulse);

        yield return new WaitForSeconds(timeBeforeHoming);
        shouldHome = true;
    }
}
