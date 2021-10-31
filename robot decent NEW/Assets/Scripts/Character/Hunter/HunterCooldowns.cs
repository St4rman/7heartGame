using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HunterCooldowns : MonoBehaviour
{
    #region  Variables

    [Header("References & Keybinds")]
    [SerializeField] public KeyCode dashKey = KeyCode.V;
    private Rigidbody rb;
    public Camera fpsCam;
    AudioSource hunterAudio;
    public AudioClip dashSound;


    [Header("Dash")]
    [SerializeField] private float dashForce; //15f
    [SerializeField] public bool canDash;
    [SerializeField] public bool isDashing;
    public float dashDuration; //5f
    

    [Header("Throwing Knife")]
    [SerializeField] public KeyCode knifeKey = KeyCode.X;
    [SerializeField] public bool hasKnife;
    public float knifeDuration; //7f


    #endregion
    
    #region MonoBehaviour Callbacks

    void Awake()
    {
        rb = transform.parent.gameObject.GetComponent<Rigidbody>();
        canDash = hasKnife = true;
        isDashing = false;
        dashDuration = 5f;
        knifeDuration = 7f;
        hunterAudio = GetComponent<AudioSource>();
    }
    
    void Update()
    {
        MyInput();
    }

    private void MyInput()
    {
        if(Input.GetKeyDown(dashKey) && canDash)
        {
            StartCoroutine(Dash());
        }

        if(Input.GetKeyDown(KeyCode.H))
        {
            setDash();
            setKnife();
        }

        if(Input.GetKeyDown(knifeKey) && hasKnife) StartCoroutine(KnifeThrow());
    }
    #endregion
    
    #region Cooldown math priv methods

    private IEnumerator Dash()
    {
        canDash = false;
        rb.AddForce(fpsCam.transform.forward * dashForce * 100f);
        isDashing =true;

        hunterAudio.clip = dashSound;
        hunterAudio.Play();
        
        yield return new WaitForSeconds(dashDuration);
        setDash();
    }

    private void setDash(){
        canDash = true;
        isDashing = false;
    }

    private IEnumerator KnifeThrow()
    {
        hasKnife = false;
        Debug.Log("Knife thrown");

        yield return new WaitForSeconds(knifeDuration);
        setKnife();
    }

    private void setKnife()
    {
        hasKnife = true;
    }
    #endregion
}
