using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval;
    public int spawnCap;

    private float nextSpawnTime;
    List<GameObject> spawnedEnemies;

    public int count;
    void Start()
    {
        spawnedEnemies = new List<GameObject>();
        nextSpawnTime = Time.time + spawnInterval;
    }

    private void Update()
    {
        foreach(var enemy in spawnedEnemies)
        {
            if(enemy == null)
            {
                spawnedEnemies.Remove(enemy);
            }
        }
    }

    void FixedUpdate()
    {
        if(Time.fixedTime > nextSpawnTime)
        {
            if (enemyPrefab != null && spawnedEnemies.Count < spawnCap)
            {
                Vector3 spawnPos = new Vector3(Random.Range(-15f, 15f), Random.Range(-15f, 15f), 0);
                var newEnemy = Instantiate(enemyPrefab, transform.position + spawnPos, Quaternion.identity);
                newEnemy.GetComponent<enemyController>().isAggro = true;

                spawnedEnemies.Add(newEnemy);
            }
            nextSpawnTime = Time.fixedTime + spawnInterval;
        }
        count = spawnedEnemies.Count;
    }

    public void ClearAllGhostsSpawned()
    {
        foreach(var entity in spawnedEnemies)
        {
            if (entity != null)
                Destroy(entity.gameObject);
        }
    }

    // Use by GameManager to set the spawn cap of each wave
    public void SetSpawnCap(int _cap) => spawnCap = _cap;

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, 15f);
    }
}
