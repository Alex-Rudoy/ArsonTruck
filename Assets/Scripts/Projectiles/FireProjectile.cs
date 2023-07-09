using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class FireProjectile : MonoBehaviour
{
    private Vector3 speed;

    [SerializeField]
    private float fireSpread = 1f;

    void Update()
    {
        transform.position += speed * Time.deltaTime;
    }

    public void SetMovementDirection(Vector3 direction)
    {
        direction += new Vector3(
            Random.Range(-fireSpread, fireSpread),
            Random.Range(-fireSpread, fireSpread),
            Random.Range(-fireSpread, fireSpread)
        );
        speed = direction;
        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.TryGetComponent<HP>(out HP hp))
            return;

        hp.TakeDamage(1);
        DestoySelf();
    }

    private void DestoySelf()
    {
        if (gameObject == null)
            return;

        Destroy(gameObject);
    }
}
