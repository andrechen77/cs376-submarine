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
    public AudioClip sonar;

    private AudioSource sonarSource;
    private float lastRetargetTime;
    private Rigidbody rb;

    internal void Start() {
        rb = GetComponent<Rigidbody>();
        lastRetargetTime = float.NegativeInfinity;
        sonarSource = GetComponentInChildren<AudioSource>();
    }

    internal void FixedUpdate() {
        if (Time.time >= lastRetargetTime + retargetInterval) {
            lastRetargetTime = Time.time;
            Retarget();

            // hijack the retarget timer for pings as well
            if (sonar != null && sonarSource != null) {
                sonarSource.Play();
            }
        }

        Vector3 direction = (currentTarget - transform.position).normalized;
        rb.AddForce(enginePower * direction);
    }

    private void Retarget() {
        if (circlingVictim == null) {
            return;
        }
        currentTarget = circlingVictim.transform.position + circlingDistance * Random.insideUnitSphere;
    }

    internal void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.TryGetComponent<Submersible>(out Submersible player)) {
            FindObjectOfType<GameMaster>().Win();
            
            Destroy(gameObject);
        }
    }
}
