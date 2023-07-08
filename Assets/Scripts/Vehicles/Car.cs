using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class Car : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed;

    private float speed;

    [SerializeField]
    private MeshRenderer meshRenderer;

    [SerializeField]
    private CarColorsSO possibleCarColors;

    [SerializeField]
    private int _damageToPlayerOnCollision;

    [SerializeField]
    private ParticleSystem[] explosions;

    public int damageToPlayerOnCollision
    {
        get => _damageToPlayerOnCollision;
    }

    private void Start()
    {
        maxSpeed += Random.Range(-2, 2);
        speed = maxSpeed;
        setRandomCarColor();
    }

    private void setRandomCarColor()
    {
        var materials = meshRenderer.materials;

        if (!possibleCarColors)
            return;

        materials[0] = possibleCarColors.carMaterials[
            Random.Range(0, possibleCarColors.carMaterials.Length)
        ];
        meshRenderer.materials = materials;
    }

    private void Update()
    {
        float rayLength = 20;

        speed += 5 * Time.deltaTime;
        speed = Mathf.Clamp(speed, 0, maxSpeed);
        transform.position += transform.forward * Time.deltaTime * speed;

        if (!Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, rayLength))
            return;

        if (hit.collider.gameObject.TryGetComponent<Car>(out Car car) == false)
            return;

        speed -= Time.deltaTime * (rayLength - hit.distance) * 0.5f;
        speed = Mathf.Clamp(speed, 0, maxSpeed);
    }

    public async void Explode()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        foreach (ParticleSystem explosion in explosions)
        {
            explosion.Play();
        }
        await Task.Delay(300);
        meshRenderer.enabled = false;
        await Task.Delay(200);
        Destroy(gameObject);
    }
}
