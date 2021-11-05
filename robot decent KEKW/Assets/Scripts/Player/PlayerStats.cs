using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] public float mainHealth;
    public float currentHealth;
    public float ratioHealth;

    public float timeSinceLastCall;

    void Awake()
    {
        currentHealth = mainHealth;
    }


    void Update()
    {
        timeSinceLastCall += Time.deltaTime;

        ratioHealth = currentHealth/mainHealth;
        
        if(timeSinceLastCall > 3f)
        {
            RegenHealth();
        }

        if(currentHealth > mainHealth) currentHealth = mainHealth;

        if(currentHealth == 0f)
        {
            Die();
        }
    }

    public void TakeDamage(float damageApplied)
    {
        currentHealth -= damageApplied;
    }

    void RegenHealth()
    {
        currentHealth += 5f * Time.deltaTime;
    }

    void Die()
    {
        Debug.Log("DEATH");
        //reload the scene poggies
    }
}
