using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GunInfo : MonoBehaviour
{
    [Header("Referencials")]
    public GameObject player;
    public Weapon curWep;

    [Header("Main Gun Ammo")]
    [SerializeField] public GameObject fullAmmoText;
    [SerializeField] public GameObject curAmmoText;

    [Header("Reloading")]
    [SerializeField] public Image RelaodUI;
    public float reloadTimeDuration;
    private Animator anim;

    public Image cringe;

    void Awake()
    {
        curWep = player.GetComponent<Weapon>();
        anim = GetComponentInChildren<Animator>();

        cringe.fillAmount = 0f;
    }

    // Update is called once per frame
    void Update(){

        var curchild = transform.GetChild(1);
        var fullchild = transform.GetChild(2);
        var nameChild = transform.GetChild(3);

        TMP_Text _tc = curchild.GetComponentInChildren<TMP_Text>();
        TMP_Text _fc = fullchild.GetComponentInChildren<TMP_Text>();
        TMP_Text _nc = nameChild.GetComponentInChildren<TMP_Text>();

        _tc.text =("" + curWep.curAmmo.ToString());
        _fc.text =("/"+ curWep.gunAmmo.ToString());
        _nc.text =("*"+ curWep.gunName.ToString());

    }

    public void reloadUI(float time)
    {
        Debug.Log("LMAO");
        anim.SetBool("reloading", true);
        Invoke("setRfalse" , time);
    }

    void setRfalse(){
        anim.SetBool("reloading",false);
    }
}
