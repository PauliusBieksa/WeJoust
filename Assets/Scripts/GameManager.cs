using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject MaskPrefab;
    private GameObject[] maskSpawnPoints;
    private GameObject[] playerSpawnPoints;
    private List<ItemSpawnPoint> maskSpawnPointScripts;
    private List<PlayerSpawnPoint> playerSpawnPointScripts;

    public bool spawnItem = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maskSpawnPoints = GameObject.FindGameObjectsWithTag("ItemSpawnPoint");
        playerSpawnPoints = GameObject.FindGameObjectsWithTag("PlayerSpawnPoint");
        maskSpawnPointScripts = new List<ItemSpawnPoint>();
        playerSpawnPointScripts = new List<PlayerSpawnPoint>();
        for (int i = 0; i < maskSpawnPoints.Length; i++)
        {
            maskSpawnPointScripts.Add(maskSpawnPoints[i].GetComponent<ItemSpawnPoint>());
        }
        for (int i = 0; i < playerSpawnPoints.Length; i++)
        {
            playerSpawnPointScripts.Add(playerSpawnPoints[i].GetComponent<PlayerSpawnPoint>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        /// deleteme
        ///////////////////////////////////////////////////////////////////////////////////////////////////
        if (spawnItem)
        {
            spawnItem = false;
            SpawnMask();
        }
        ///////////////////////////////////////////////////////////////////////////////////////////////////
    }

    private void SpawnMask()
    {
        int index = Random.Range(0, maskSpawnPoints.Length);
        for (int i = 0; i < maskSpawnPointScripts.Count; i++)
        {
            // Spawn if spawn point empty
            if (maskSpawnPointScripts[index].SpawnedMask == null)
            {
                maskSpawnPointScripts[index].SpawnedMask = Instantiate(MaskPrefab);
                maskSpawnPointScripts[index].SpawnedMask.transform.position = maskSpawnPointScripts[index].transform.position;
                break;
            }
            else
            {
                index = index < maskSpawnPointScripts.Count - 1 ? index + 1 : 0;
            }
        }
    }

    public Vector3 GetSpawnPosition()
    {
        for (int i = 0; i < playerSpawnPointScripts.Count; i++)
        {
            // Spawn if spawn point empty
            if (!playerSpawnPointScripts[i].HasPlayer)
            {
                playerSpawnPointScripts[i].HasPlayer = true;
                return playerSpawnPoints[i].transform.position;
            }
        }
        return Vector3.zero;
    }

    public void ResetGame()
    {
        for (int i = 0;i < playerSpawnPointScripts.Count; i++)
        {
            playerSpawnPointScripts[i].HasPlayer = false;
        }
        for (int i = 0; i < maskSpawnPointScripts.Count; i++)
        {
            if (maskSpawnPointScripts[i].SpawnedMask != null)
                Destroy(maskSpawnPointScripts[i].SpawnedMask);
        }
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            Destroy(player);
        }
    }
}
