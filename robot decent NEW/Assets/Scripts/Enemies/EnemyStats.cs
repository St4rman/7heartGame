using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] public float lookRadius; //chjangeable
    [SerializeField] public float meleeRange;
    [SerializeField] public float maxHPoints;

    [SerializeField] public float turretFireRate; //time between bullets
    [SerializeField] public float turretBulletSpeed; //speed of bullets
    [SerializeField] public float turretDamage;
    [SerializeField] public float meleeDamage =10f; //changeable
    [SerializeField] public float meleeAtkRate; //changeable


    private float awakeTime;
    
    [SerializeField] public LayerMask detectionLayer;

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, meleeRange);
    }

    void Update()
    {
        awakeTime += Time.deltaTime;

        if (awakeTime > 10f)
        {
            IncreaseWave();
            awakeTime = 0f;
        }
        if(turretBulletSpeed > 200f)
        {
            turretBulletSpeed = 200f;
        }
    }
    private void waveController()
    {

    }

    private void IncreaseWave()
    {
        Debug.Log("in inc wave");

        if(turretFireRate >= 1f){
            turretFireRate +=  -1f;
        }

        if(turretFireRate == 1f){
            turretBulletSpeed += 30f;
            if (turretBulletSpeed == 200f) {turretFireRate = 0.5f;}
        }

        if(lookRadius < 80f){
            lookRadius += 20f;
        }

    }
}
