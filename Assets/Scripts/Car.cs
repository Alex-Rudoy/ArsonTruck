using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField]
    private float maxSpeed;

    private float speed;

    [SerializeField]
    private MeshRenderer meshRenderer;

    [SerializeField]
    private CarColorsSO possibleCarColors;

    private void Start()
    {
        maxSpeed += Random.Range(-2, 2);
        speed = maxSpeed;
        setRandomCarColor();
    }

    private void setRandomCarColor()
    {
        var materials = meshRenderer.materials;
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
}
