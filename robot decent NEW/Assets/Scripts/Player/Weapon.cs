using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    #region Variables

    public Gun[] loadout;
    public Transform weaponParent;
    public Transform orientation;

    private GameObject curWeapon;
    private int equippedIndex;

    AudioSource playerAudio;

    GameObject newWeapon;

    [SerializeField] LayerMask canBeShot;
    [SerializeField]public GameObject bulletHole;

    [Header("Assignables")]
    [SerializeField] KeyCode primWeaponKey = KeyCode.Alpha1;
    [SerializeField] KeyCode secWeaponKey = KeyCode.Alpha2;
    [SerializeField] GunInfo gunUI;
    public Camera fpsCam;
    public int curAmmo;

    [Header("Gun Variables")]
    float gunRange;
    float gunBloom;
    float gunRecoil;
    float gunKickback;
    float gunCooldown;
    int gunDamage;
    public string gunName;

    public int gunreloadTime;
    public int gunAmmo;

    public bool isReloading;

    private LineRenderer laserLine; 
    public Transform gunMuzzle;

    #endregion


    #region Monobehaviour Callbacks
    void Start()
    {
        equippedIndex = 9; //only so our equip function works and cant spam 1 to insta reload

        laserLine = GetComponent<LineRenderer>(); 

        playerAudio = GetComponent<AudioSource>();
    }


    void Update()
    {
        

        if(Input.GetKeyDown(primWeaponKey) && equippedIndex!=0) Equip(0);
        //if(Input.GetKeyDown(secWeaponKey) && equippedIndex !=1) Equip(1);

        
        if(curWeapon != null)
        {
            Aim(Input.GetMouseButton(1));

            if(Input.GetMouseButtonDown(0) && gunCooldown <= 0)
            {
                if(curAmmo > 0){

                    //AMMO count
                    curAmmo -= 1;
                    Shoot();
                    isReloading = false;
                }
                else
                {
                    StartCoroutine(Reload());
                }
                
            }

            //weapon position elasticity
            curWeapon.transform.localPosition = Vector3.Lerp(curWeapon.transform.localPosition, Vector3.zero, Time.deltaTime * 2f);

            //ROF
            if(gunCooldown > 0 ) gunCooldown -= Time.deltaTime;

            //if(curAmmo == 0)StartCoroutine(Reload()); //idk why this bullshit added extra bs
        }

        
    }

    #endregion

    #region Private Methods
    void Equip (int p_Index)
    {
        if(curWeapon != null) Destroy (curWeapon);

        equippedIndex = p_Index;


        GameObject newWeapon =  Instantiate (loadout[equippedIndex].prefab, weaponParent.position, orientation.rotation, weaponParent) as GameObject;
        newWeapon.transform.localPosition = Vector3.zero;
        newWeapon.transform.localEulerAngles = Vector3.zero;

        curWeapon = newWeapon;

        
        #region gunVar
        gunName = loadout[equippedIndex].name;
        gunRange = loadout[equippedIndex].range;
        gunBloom = loadout[equippedIndex].bloom;
        gunRecoil = loadout[equippedIndex].recoil;
        gunKickback = loadout[equippedIndex].kickback;
        gunDamage = loadout[equippedIndex].damage;
        gunAmmo = loadout[equippedIndex].ammo;
        gunreloadTime = loadout[equippedIndex].reloadTime;
        #endregion

        curAmmo = gunAmmo;

    }

    void Aim(bool isAiming)
    {
        Transform anchorTransform = curWeapon.transform.Find("Anchor");
        Transform hipfireTransform = curWeapon.transform.Find("State/Hip");
        Transform aimdownTransform = curWeapon.transform.Find("State/ADS");
        
        
        if(isAiming)
        {
            anchorTransform.position = Vector3.Lerp(anchorTransform.position, aimdownTransform.position, Time.deltaTime * loadout[equippedIndex].aimSpeed);

        }else{

            anchorTransform.position = Vector3.Lerp(anchorTransform.position, hipfireTransform.position, Time.deltaTime * loadout[equippedIndex].aimSpeed);
        }

    }

    void Shoot()
    {
        //we use the fpscam for the transform of the ray 
        Transform bulletSpawn = fpsCam.transform; 

        //Bloom
        Vector3 bloomTransform = bulletSpawn.position + bulletSpawn.forward * gunRange; 
        bloomTransform += Random.Range(-gunBloom, gunBloom) * bulletSpawn.up;
        bloomTransform += Random.Range(-gunBloom, gunBloom) * bulletSpawn.right;
        bloomTransform -= bulletSpawn.position;
        bloomTransform.Normalize();

        //Raycast
        RaycastHit bHit = new RaycastHit();

        //visual effects poggers
        StartCoroutine(ShotEffect()); 
        Transform gunEndTransform = curWeapon.transform.Find("State/muzzle"); 
        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3 (.5f, .5f, 0));
        laserLine.SetPosition(0, gunEndTransform.position);

        //actual shooting
        if(Physics.Raycast(bulletSpawn.position, bloomTransform, out bHit, gunRange, canBeShot))
        {
            
            GameObject newBulletHole = Instantiate(bulletHole, bHit.point + bHit.normal * 0.001f, Quaternion.identity) as GameObject;
            newBulletHole.transform.LookAt(bHit.point + bHit.normal);
            Destroy(newBulletHole, 2f);
            
            laserLine.SetPosition (1, bHit.point); 

            //HIT ENEMY
            //Enemy enemy = bHit.transform.GetComponent<Enemy>();
            
            Shootable enemy = bHit.transform.GetComponent<Shootable>();
            if(enemy != null)
            {
                enemy.TakeDamage(gunDamage);
            }

        } else {
            laserLine.SetPosition( 1, rayOrigin + (fpsCam.transform.forward * gunRange));
        }

        //gun effects
        curWeapon.transform.Rotate(-gunRecoil, 0, 0);
        curWeapon.transform.position -= curWeapon.transform.forward * gunKickback;
        //the weapon sway script puts the rotate back where it needs to be thats why it resets lol

        //ROF setting this here because it needs to reset every shot
        gunCooldown = (60/loadout[equippedIndex].fireRate);

        //Gun sound
        playerAudio.clip = loadout[equippedIndex].shootSound;
        playerAudio.Play();

    }
    
    private IEnumerator Reload()
    { 
        gunUI.reloadUI(gunreloadTime);
        yield return new WaitForSeconds(gunreloadTime);
        isReloading= true;
        curAmmo = gunAmmo;
    }

    private IEnumerator ShotEffect() 
    {
        laserLine.enabled = true;
        yield return new WaitForSeconds(0.5f);
        laserLine.enabled = false;
    }
    
    #endregion
}
