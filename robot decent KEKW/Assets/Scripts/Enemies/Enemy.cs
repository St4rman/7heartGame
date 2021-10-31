using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private float curHealth;
    private float attackRange;
    bool canAttack;
    public float damage;
    private float distanceToTarget = Mathf.Infinity;

    [SerializeField] EnemyStats myStats;
    [SerializeField] Transform target;
    [SerializeField] PlayerStats playerStatReference;
    [SerializeField] public float attackRate = 3f;
    NavMeshAgent navMeshAgent;
    
    private GameObject player;

    void Awake()
    {
        EnemyStats myStats = GetComponent<EnemyStats>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        target = GameObject.Find("BasePlayer").transform;
        
        playerStatReference = GameObject.FindObjectOfType<PlayerStats>();
        
        canAttack = true;
    }

    void Start()
    {
        curHealth = myStats.maxHPoints;
        attackRange = myStats.meleeRange;
        damage = myStats.meleeDamage;
    }
    void Update()
    {   
        distanceToTarget = Vector3.Distance(target.position, transform.position);

        if(distanceToTarget <= myStats.lookRadius && distanceToTarget > attackRange)  
        {
            var rotationVector = transform.rotation.eulerAngles;
            rotationVector.x = 15;
            transform.rotation = Quaternion.Euler(rotationVector);

            navMeshAgent.SetDestination(target.position);
        }

        if(distanceToTarget<=attackRange && canAttack) {

            
            navMeshAgent.stoppingDistance = attackRange;
            StartCoroutine(attackTarget());
        }

    }

    IEnumerator attackTarget()
    {
        canAttack = false;
        

        //do something and time attacks here lol

        player = GameObject.Find("BasePlayer");
        playerStatReference = player.GetComponent<PlayerStats>();

        playerStatReference.TakeDamage(damage);
        yield return new WaitForSeconds(myStats.meleeAtkRate);
        canAttack = true;

    }

    /*public void TakeDamage(int dmg){

        curHealth -= dmg;
        Debug.Log(curHealth);

        if(curHealth <= 0)
        {
            Dead();
        }
    }

    private void Dead()
    {
        Destroy(gameObject);
    }*/

}
