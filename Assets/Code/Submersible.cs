using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Submersible : MonoBehaviour {
    public float enginePower;
    public float fuel;
    public float rollInput;
    public float pitchSpeed;
    public float sidewaysDragFactor;
    public AudioClip explosionSound;
    public float fuelSpawnInterval;
    public float fuelSpawnRadius;
    public GameObject fuelPrefab;

    private float timeSinceLastFuelSpawn;

    private GameMaster gameMaster;

    Rigidbody rb;

    internal void Start() {
        rb = GetComponent<Rigidbody>();
        timeSinceLastFuelSpawn = float.NegativeInfinity;
        gameMaster = FindObjectOfType<GameMaster>();
    }

    internal void FixedUpdate() {
        float thrustInput = Input.GetAxis("Jump");
        float rollInput = Input.GetAxis("Horizontal");
        float pitchInput = Input.GetAxis("Vertical");

        Thrust(thrustInput);

        float forwardVel = Vector3.Dot(rb.velocity, transform.up);

        float rollTorque = -forwardVel * rollInput * this.rollInput;
        rb.AddTorque(transform.up * rollTorque);
        float pitchTorque = forwardVel * pitchInput * pitchSpeed;
        rb.AddTorque(transform.right * pitchTorque);

        Vector3 sidewaysVel = rb.velocity - forwardVel * transform.up;
        rb.AddForce(-sidewaysDragFactor * sidewaysVel);
    }

    internal void OnCollisionEnter(Collision collision) {
        if (explosionSound != null) {
            AudioSource.PlayClipAtPoint(explosionSound, transform.position);
        }
        if (!collision.gameObject.GetComponent<Target>()) {
            gameMaster.Lose();
        }
        Destroy(gameObject, 0.1f);
    }

    internal void Update() {
        if (fuel == 0 && rb.velocity.sqrMagnitude < 0.01) {
            gameMaster.Lose();
        }

        gameMaster.IndicateFuel(fuel);

        if (Time.time > timeSinceLastFuelSpawn + fuelSpawnInterval) {
            timeSinceLastFuelSpawn = Time.time;
            
            Vector3 spawnAt = transform.position + fuelSpawnRadius * Random.insideUnitSphere;
            if (spawnAt.y < 0) {
                spawnAt.y = -spawnAt.y;
            }
            Instantiate(fuelPrefab, spawnAt, Quaternion.identity);
        }
    }

    internal void Thrust(float thrustInput) {
        fuel -= Mathf.Abs(thrustInput);
        if (fuel > 0) {
            rb.AddForce(thrustInput * enginePower * transform.up);
        } else {
            fuel = 0;
        }
    }

    public void AddFuel(float deltaFuel) {
        fuel += deltaFuel;
    }

    void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward); // Z-axis
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position + transform.up); // Y-axis
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.right); // X-axis
    }
}
