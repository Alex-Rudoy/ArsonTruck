using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject roadPrefab;
    public GameObject crossroadPrefab;

    private List<GameObject> roadBlocks = new List<GameObject>();

    [SerializeField]
    private GameObject[] carPrefabs;

    private void Start()
    {
        for (int i = -2; i < 2; i++)
        {
            CreateNewRoadBlock(i);
        }

        Player.OnMilestoneReached += UpdateRoadBlocks;
    }

    private void UpdateRoadBlocks(object sender, Player.OnMilestoneReachedEventArgs e)
    {
        if (e.milestone % 6 == 0)
        {
            CreateNewCrossroad(e.milestone);
            SpawnLeftCars(e.milestone);
            SpawnRightCars(e.milestone);
        }
        else
        {
            CreateNewRoadBlock(e.milestone);
        }
        SpawnForwardCars(e.milestone);
        SpawnBackCars(e.milestone);
        RemoveOldRoadBlock();
    }

    private void CreateNewRoadBlock(int milestone)
    {
        GameObject newRoadBlock = Instantiate(
            roadPrefab,
            new Vector3(0, 0, 30 + milestone * 30),
            Quaternion.identity
        );
        roadBlocks.Add(newRoadBlock);
    }

    private void CreateNewCrossroad(int milestone)
    {
        GameObject newRoadBlock = Instantiate(
            crossroadPrefab,
            new Vector3(0, 0, 30 + milestone * 30),
            Quaternion.identity
        );
        roadBlocks.Add(newRoadBlock);
    }

    private void SpawnForwardCars(int milestone)
    {
        int randomAmountOfCars = Random.Range(0, 2);
        for (int i = 0; i < randomAmountOfCars; i++)
        {
            float randomLane = Random.Range(0, 2);
            GameObject newCar = Instantiate(
                carPrefabs[Random.Range(0, carPrefabs.Length)],
                new Vector3(2.5f + randomLane * 5, 0, 15 + milestone * 30 + Random.Range(0, 30)),
                Quaternion.identity
            );
        }
    }

    private void SpawnBackCars(int milestone)
    {
        if (milestone % 3 == 0)
        {
            float randomLane = Random.Range(0, 2);
            GameObject newCar = Instantiate(
                carPrefabs[Random.Range(0, carPrefabs.Length)],
                new Vector3(-2.5f - randomLane * 5, 0, 15 + milestone * 30 + Random.Range(0, 60)),
                Quaternion.identity
            );
            newCar.transform.Rotate(0, 180, 0);
        }
    }

    private void SpawnLeftCars(int milestone)
    {
        // spawn cars that go from left to right on the upcoming crossroad prefab

        int randomAmountOfCars = Random.Range(2, 4);
        for (int i = 0; i < randomAmountOfCars; i++)
        {
            float randomLane = Random.Range(0, 2);
            GameObject newCar = Instantiate(
                carPrefabs[Random.Range(0, carPrefabs.Length)],
                new Vector3(Random.Range(0, -80), 0, 30 + milestone * 30 - 2.5f - randomLane * 5),
                Quaternion.identity
            );
            newCar.transform.Rotate(0, 90, 0);
        }
    }

    private void SpawnRightCars(int milestone)
    {
        int randomAmountOfCars = Random.Range(2, 4);
        for (int i = 0; i < randomAmountOfCars; i++)
        {
            float randomLane = Random.Range(0, 2);
            GameObject newCar = Instantiate(
                carPrefabs[Random.Range(0, carPrefabs.Length)],
                new Vector3(Random.Range(0, 80), 0, 30 + milestone * 30 + 2.5f + randomLane * 5),
                Quaternion.identity
            );
            newCar.transform.Rotate(0, -90, 0);
        }
    }

    private void RemoveOldRoadBlock()
    {
        Destroy(roadBlocks[0]);
        roadBlocks.RemoveAt(0);
    }
}