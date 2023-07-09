using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StraightRoad : MonoBehaviour
{
    [SerializeField]
    private HousesSO housePrefabs;

    void Start()
    {
        GameObject house1 = Instantiate(
            housePrefabs.housePrefabs[Random.Range(0, housePrefabs.housePrefabs.Length)],
            new Vector3(25, 0, transform.position.z),
            Quaternion.Euler(0, Random.Range(0, 4) * 90, 0)
        );

        GameObject house2 = Instantiate(
            housePrefabs.housePrefabs[Random.Range(0, housePrefabs.housePrefabs.Length)],
            new Vector3(-25, 0, transform.position.z),
            Quaternion.Euler(0, Random.Range(0, 4) * 90, 0)
        );
    }
}
