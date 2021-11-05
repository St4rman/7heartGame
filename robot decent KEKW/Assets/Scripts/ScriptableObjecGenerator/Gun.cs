using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Gun", menuName = "Gun")]
public class Gun : ScriptableObject
{
    public string name;
    public int gunIndex;
    public GameObject prefab;

    [Header("Gun Stats")]

    public float fireRate;
    public float aimSpeed;
    public float range;
    public int damage;
    public int ammo;
    public int reloadTime;

    [Header("Flair")]
    public float bloom;
    public float kickback;
    public float recoil;

    public AudioClip shootSound;

}
