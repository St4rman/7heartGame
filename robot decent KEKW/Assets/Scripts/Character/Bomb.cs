using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public GameObject bombPrefab;
    private Rigidbody rb;
   
    public Transform orientation;
    bool canBomb;
    public Camera fpsCam;
    public Transform bombOrigin;
    [SerializeField] KeyCode bombKey;
    [SerializeField] float bombTimer;
    [SerializeField] float bombVelocity = 40f;

    void Awake(){

    }
    void Update()
    {
        bombTimer += Time.deltaTime;

        if(bombTimer > 3f)
        {
            canBomb = true;
        }

        if(Input.GetKeyDown(bombKey) && canBomb){
            throwBomb();
        }
    }

    void throwBomb(){
        bombTimer = 0f;
        canBomb = false;
        
        //instantiate bomb
        GameObject bombIns =  Instantiate(bombPrefab, transform.position, orientation.rotation);
        //apply velocity to the bomb 
        Rigidbody rb1 = bombIns.GetComponent<Rigidbody>();
        rb1.AddForce(fpsCam.transform.forward * bombVelocity, ForceMode.VelocityChange);
         
        Debug.Log("BVOMB!");
    }
}
