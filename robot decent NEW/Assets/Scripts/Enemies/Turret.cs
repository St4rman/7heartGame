using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] GameObject bullet;
    [SerializeField] private Transform barrel;
    [SerializeField] float timebetbullets;
    [SerializeField] float velocity;
    public bool canShoot;

    private LineRenderer sight;


    void Awake()
    {
        sight = GetComponent<LineRenderer>();
        EnemyStats myStats = GetComponent<EnemyStats>();

        timebetbullets = myStats.turretFireRate;
        velocity = myStats.turretBulletSpeed;

        canShoot = false;
        StartCoroutine(WaitAwake());
    }


    IEnumerator WaitAwake()
    {
        yield return new WaitForSeconds(3.0f);
        shootPlayer();
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if(target != null)
        {
            transform.LookAt(target);

            if(canShoot){
                shootPlayer();
            }
            
        }
    }

    IEnumerator checkIfShoot()
    {
        Vector3 toPlayerDir = (target.position - transform.position).normalized;

        var projectileInstance =  Instantiate(bullet, barrel.position, Quaternion.LookRotation(transform.forward));

        if(projectileInstance.TryGetComponent(out Rigidbody rb))
        {
            rb.useGravity = false;
            rb.velocity = toPlayerDir * velocity;
        }

        yield return new WaitForSeconds(timebetbullets);
        canShoot = true;
    }



    void shootPlayer()
    {
        canShoot = false;
        StartCoroutine(checkIfShoot());
        
    }

}
