using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    private Transform flameThrower;

    [SerializeField]
    private Transform emissionPoint;

    [SerializeField]
    private GameObject projectilePrefab;

    // bullets per second
    [SerializeField]
    private float fireRate;

    private float nextTimeToFire = 0f;

    private void Update()
    {
        nextTimeToFire += Time.deltaTime;
        UpdateFlameThrowerDirection();

        while (nextTimeToFire > 0)
        {
            Shoot();
            nextTimeToFire -= 1 / fireRate;
        }
    }

    private void UpdateFlameThrowerDirection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            flameThrower.LookAt(new Vector3(hit.point.x, flameThrower.position.y, hit.point.z));
        }
    }

    private void Shoot()
    {
        GameObject projectile = Instantiate(
            projectilePrefab,
            emissionPoint.position,
            flameThrower.rotation
        );
        var playerMovementVector = transform.forward * gameObject.GetComponent<Player>().Speed;
        var flameThrowerVector = flameThrower.forward * 30;
        projectile
            .GetComponent<Projectile>()
            .SetMovementDirection(flameThrowerVector + playerMovementVector / 2);
    }
}
