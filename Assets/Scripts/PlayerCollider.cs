using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Car>(out Car car))
        {
            car.Explode();
        }
    }
}
