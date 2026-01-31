using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject MaskPrefab;
    private GameObject[] maskSpawnPoints;
    private List<ItemSpawnPoint> maskSpawnPointScripts;

    public bool spawnItem = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        maskSpawnPoints = GameObject.FindGameObjectsWithTag("ItemSpawnPoint");
        maskSpawnPointScripts = new List<ItemSpawnPoint>();
        for (int i = 0; i < maskSpawnPoints.Length; i++)
        {
            maskSpawnPointScripts.Add(maskSpawnPoints[i].GetComponent<ItemSpawnPoint>());
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
}
