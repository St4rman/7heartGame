using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : MonoBehaviour
{
    [SerializeField] EnemyStats myStats;
    public float curHealth;
    
    
    void Awake()
    {
        //EnemyStats myStats = this.gameObject.FindObjectOfType<EnemyStats>();
    }

    void Start()
    {
        curHealth = 80;
    }

    // Update is called once per frame
    void Update()
    {
        if (curHealth <=0) Death();
        //Debug.Log(curHealth);
    }

    public void TakeDamage(int dmg){

        curHealth -= dmg;
        
    }

    public void Death()
    {
        if (gameObject.name == "turret")
        {
            gameObject.SetActive(false);
        }
        else{
            Destroy(gameObject);
        }
    }
}
