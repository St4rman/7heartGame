using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] public float bulletDamage;

    void OnCollisionEnter(Collision col){
        if(col.gameObject.tag == "Player")
        {
            Debug.Log("got shot");

            PlayerStats playerStatReference = col.transform.GetComponent<PlayerStats>();
            playerStatReference.TakeDamage(bulletDamage);
        }
        if(col.gameObject.tag != "Enemy"){
            Object.Destroy(this.gameObject);
        }
    }
}
