using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothSpeed;

    void LateUpdate() {
        if (target == null)
            return;

        Vector3 targetPosition = target.position + target.TransformDirection(offset);

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);

        transform.LookAt(target.position, target.forward);
    }
}
