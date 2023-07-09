using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Bullet : MonoBehaviour
{
    private int speed = 70;

    private void Start()
    {
        Destroy(gameObject, 5);
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.TryGetComponent<Player>(out Player player))
            return;

        player.gameObject.GetComponent<HP>().TakeDamage(5);
        DestoySelf();
    }

    private void DestoySelf()
    {
        if (gameObject == null)
            return;

        Destroy(gameObject);
    }
}
