﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //Can be private with SerializeField
    public GameObject topSpawn;
    public GameObject leftSpawn;
    public GameObject rightSpawn;
    public GameObject botSpawn;
    [SerializeField]
    private GameObject enemyPrefab; //enemy prefab
    [Range (1, 100)] public int maxEnemies = 30;
    public int currentEnemies;
    [SerializeField][Tooltip("This value CANNOT be 0, I inversed it")]
    private float spawnRate; //The higher the value, the faster the spawn rate
    
    private void Start()
    {
        StartCoroutine(Spawner(1/spawnRate));
        currentEnemies = 0;
    }
    IEnumerator Spawner(float spawnRate)
    { 
        while (currentEnemies < maxEnemies)
        {
            //Get a new randomized range of values each spawn
            float topSpawnPos = topSpawn.transform.position.x + Random.Range(-12f,12f);
            float botSpawnPos = botSpawn.transform.position.x + Random.Range(-12f,12f);
            float leftSpawnPos = leftSpawn.transform.position.y + Random.Range(-8f,8f);
            float rightSpawnPos = rightSpawn.transform.position.y + Random.Range(-8f,8f);
            int rand = Random.Range (0,4); //Randomize the spawn points
            //Debug.Log(rand);

            switch (rand)
            {
                case 0: //Spawns using the top spawnpoint
                    Instantiate(enemyPrefab, new Vector2(topSpawnPos, topSpawn.transform.position.y), Quaternion.identity);
                    currentEnemies++;
                    yield return new WaitForSeconds(spawnRate);
                break;

                case 1: //Spawns using the bottom spawnpoint
                    Instantiate(enemyPrefab, new Vector2(botSpawnPos, botSpawn.transform.position.y), Quaternion.identity);
                    currentEnemies++;
                    yield return new WaitForSeconds(spawnRate);
                break;

                case 2: //Spawns using the right spawnpoint
                    Instantiate(enemyPrefab, new Vector2(rightSpawn.transform.position.x, rightSpawnPos), Quaternion.identity);
                    currentEnemies++;
                    yield return new WaitForSeconds(spawnRate);
                break;

                case 3: //Spawns using the left spawnpoint
                    Instantiate(enemyPrefab, new Vector2(leftSpawn.transform.position.x, leftSpawnPos), Quaternion.identity);
                    currentEnemies++;
                    yield return new WaitForSeconds(spawnRate);
                break;
            }
            //; What is this for? ~Dave

            
        }
        
    }
}