using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explode : MonoBehaviour
{
    Collider bCol;
    float timer;
    Rigidbody rigidbody;
    
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Physics.IgnoreLayerCollision(0,3);
    }
    void Update(){
        Explode();
    }


    // void OnCollisionEnter(Collision col){
        
    //     //play animation particle

    //     if(col.gameObject.layer == 8 ){
    //         Shootable shootable = col.gameObject.GetComponent<Shootable>();
    //         if(shootable !=null){
    //             shootable.TakeDamage(80);
    //         }
    //     }
    // }

    void Explode(){
        Collider[] hitcolliders = Physics.OverlapSphere(transform.position, 5f);
        foreach(var hitcollider in hitcolliders)
        {
            if(hitcollider.gameObject.layer == 8 ){
                Shootable shootable = hitcollider.gameObject.GetComponent<Shootable>();
                if(shootable !=null){
                    shootable.TakeDamage(80);
                }
            }
        }
    }
}
