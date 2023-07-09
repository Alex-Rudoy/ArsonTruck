using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDestroyer : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Car>(out Car car))
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.TryGetComponent<FireProjectile>(out FireProjectile projectile))
        {
            Destroy(other.gameObject);
        }
    }
}
