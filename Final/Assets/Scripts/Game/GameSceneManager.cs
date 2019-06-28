using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SWNetwork;

public class GameSceneManager : MonoBehaviour
{
    public float RespawnDelay = 3f;
    public GameObject PlayerPrefab;
    public Vector3 RespawnPosition = new Vector3(25, 2, 25);
    public Dictionary<string, int> Scores = new Dictionary<string, int>();

    const string SCORES = "Scores";

    public void OnSpawnerReady(bool finishedSetup)
    {
        if (!finishedSetup)
        {
            if (NetworkClient.Instance.IsHost)
            {
                NetworkClient.Instance.LastSpawner.SpawnForPlayer(0, 0);
            }
            else
            {
                NetworkClient.Instance.LastSpawner.SpawnForPlayer(0, 1);
            }

            NetworkClient.Instance.LastSpawner.PlayerFinishedSceneSetup();
        }
    }

    public void PlayerScored(string playerId)
    {
        if (Scores.ContainsKey(playerId))
        {
            int kill = Scores[playerId];
            Scores[playerId] = kill + 1;
        }
        else
        {
            Scores[playerId] = 1;
        }
    }

    public void DelayedRespawnPlayer()
    {
        StartCoroutine(RespawnPlayer(RespawnDelay));
    }

    IEnumerator RespawnPlayer(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);

        int spawnPointIndex = Random.Range(0, 12);
        Instantiate(PlayerPrefab, RespawnPosition, Quaternion.identity);
    }
}
