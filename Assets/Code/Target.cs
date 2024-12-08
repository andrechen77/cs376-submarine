using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float retargetInterval;
    public Vector3 currentTarget;
    public GameObject circlingVictim;
    public float circlingDistance;
    public float enginePower;

    private float lastRetargetTime;
    private Rigidbody rb;

    internal void Start() {
        rb = GetComponent<Rigidbody>();
        lastRetargetTime = float.NegativeInfinity;
    }

    internal void Update() {
        if (Time.time >= lastRetargetTime + retargetInterval) {
            lastRetargetTime = Time.time;
            Retarget();
        }

        Vector3 direction = (currentTarget - transform.position).normalized;
        rb.AddForce(enginePower * direction);
    }

    private void Retarget() {
        Debug.Log("retargeting");
        currentTarget = circlingVictim.transform.position + circlingDistance * Random.insideUnitSphere;
    }

    internal void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.TryGetComponent<Submersible>(out Submersible player)) {
            Destroy(gameObject);
        }
    }
}
