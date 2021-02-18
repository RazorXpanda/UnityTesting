using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    List<GameObject> spawnerList;

    private void Start()
    {
        spawnerList = new List<GameObject>();
        foreach(var spawner in GameObject.FindGameObjectsWithTag("Spawner"))
        {
            spawnerList.Add(spawner);
        }
    }

    public void EnableAllSpawners()
    {
        foreach (var spawner in spawnerList)
            spawner.SetActive(true);
    }
    public void DisableAllSpawners()
    {
        foreach (var spawner in spawnerList)
            spawner.SetActive(false);
    }
}
