using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {
    public float fuel;

    internal void OnTriggerEnter(Collider other) {
        if (other.gameObject.TryGetComponent<Submersible>(out Submersible missile)) {
            missile.AddFuel(fuel);
        }
        Destroy(gameObject);
    }
}
