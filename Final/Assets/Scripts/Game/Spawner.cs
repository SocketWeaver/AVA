using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> Prefabs;

    public float SpawnInterval = 30f;

    public float InitialDelay = 30f;

    public Transform SpawnPoint;

    public int MaxNumberOfSpawnedGameObject = 10;

    void Start()
    {
        InvokeRepeating("Spawn", 0, SpawnInterval);
    }

    void Spawn()
    {
        if(MaxNumberOfSpawnedGameObject > 0)
        {
            MaxNumberOfSpawnedGameObject--;

            int prefabCount = Prefabs.Count;

            int index = Random.Range(0, prefabCount - 1);

            Instantiate(Prefabs[index], SpawnPoint.position, SpawnPoint.rotation);
        }
    }
}
