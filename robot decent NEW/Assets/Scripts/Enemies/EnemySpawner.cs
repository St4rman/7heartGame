using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    float waveTimer = 5f;
    float timeSinceSpawn;
    private EnemyPool enemyPool;
    int waveCount;
    bool start;
    public Transform[] spawnPoints;

    void Start()
    {
        enemyPool = FindObjectOfType<EnemyPool>();
    }
    void Awake()
    {
        start = false;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            start = true;
        }

        timeSinceSpawn += Time.deltaTime;

        if(timeSinceSpawn>= waveTimer && start)
        {
            if(waveCount !=10){

                WaveMaker(waveCount);
                waveCount += 1;
                Debug.Log(waveCount);
                timeSinceSpawn = 0f;
            }
        }
    }

    void WaveMaker(int n)
    {
        for ( int i = 0; i< n; i++ )
        {
            GameObject newEnemy = enemyPool.GetEnemy();
            enemyPool.GetEnemy();

            newEnemy.transform.position = spawnPoints[Random.Range(0,3)].position;
            //set transform to a random corner of the arena poggers 
        }
    }
}
