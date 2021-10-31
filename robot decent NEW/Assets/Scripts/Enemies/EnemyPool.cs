using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    [SerializeField]
    private GameObject followEnemyPrefab;
    [SerializeField]
    private Queue<GameObject> enemyPool = new Queue<GameObject>();
    [SerializeField]
    private int poolStartSize;

    void Start()
    {
        for( int i = 0; i< poolStartSize; i++)
        {
            GameObject fEnemy = Instantiate(followEnemyPrefab);
            enemyPool.Enqueue(fEnemy);
            fEnemy.SetActive(false);
        }
    }

    public GameObject GetEnemy(){
        if(enemyPool.Count>0)
        {
            GameObject fEnemy = enemyPool.Dequeue();
            fEnemy.SetActive(true);
            return fEnemy;
        }
        else 
        {
            GameObject fEnemy = Instantiate(followEnemyPrefab);
            return fEnemy;
        }
    }

    public void ReturnEnemy(GameObject fEnemy)
    {
        enemyPool.Enqueue(fEnemy);
        fEnemy.SetActive(false);
    }
}
