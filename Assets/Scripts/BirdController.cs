using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    [SerializeField] private List<GameObject> birdPrefabs;
    [SerializeField] private float birdSpawnInitDelay = 20.0f;
    [SerializeField] private float birdSpawnInterval = 10.0f;

    private void Start()
    {
        InvokeRepeating("SpawnBird", birdSpawnInitDelay, birdSpawnInterval);
    }

    void SpawnBird()
    {
        int choice = Random.Range(0, birdPrefabs.Count);
        Instantiate(birdPrefabs[choice], transform);
    }
}
