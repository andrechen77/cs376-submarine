using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submersible : MonoBehaviour {
    public float enginePower;

    Rigidbody rb;

    internal void Start() {
        rb = GetComponent<Rigidbody>();
    }

    internal void FixedUpdate() {
        float thrustInput = Input.GetAxis("Jump");
        float rollInput = Input.GetAxis("Horizontal");
        float pitchInput = Input.GetAxis("Vertical");

        rb.AddForce(thrustInput * enginePower * transform.up);

        float forwardVel = Vector3.Dot(rb.velocity, transform.up);

        float rollTorque = -forwardVel * rollInput;
        rb.AddTorque(transform.up * rollTorque);
        float pitchTorque = forwardVel * pitchInput;
        rb.AddTorque(transform.right * pitchTorque);
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
