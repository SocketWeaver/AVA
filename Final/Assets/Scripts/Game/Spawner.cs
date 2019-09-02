using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWNetwork;

public class Spawner : MonoBehaviour
{
    public List<GameObject> Prefabs;

    public float SpawnInterval = 30f;

    public float InitialDelay = 30f;

    public Transform SpawnPoint;

    public int MaxNumberOfSpawnedGameObject = 10;

    SceneSpawner sceneSpawner;

    void Start()
    {
        sceneSpawner = GetComponent<SceneSpawner>();
    }

    public void OnSpawnerReady(bool finishedSceneSetup, SceneSpawner sceneSpawner)
    {
        InvokeRepeating("Spawn", 0, SpawnInterval);
    }

    void Spawn()
    {
        if (MaxNumberOfSpawnedGameObject > 0)
        {
            MaxNumberOfSpawnedGameObject--;

            if (NetworkClient.Instance.IsHost)
            {
                sceneSpawner.SpawnForNonPlayer(0, 0);
            }

            //int prefabCount = Prefabs.Count;

            //int index = Random.Range(0, prefabCount - 1);

            //Instantiate(Prefabs[index], SpawnPoint.position, SpawnPoint.rotation);
        }
    }
}


