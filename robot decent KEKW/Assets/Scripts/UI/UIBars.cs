using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIBars : MonoBehaviour
{
    public Image dashUI;/*, knifeUI*/
    public float dashTime;/*, knifeTime*/

    float to = 1f;
    float from = 0.1f;
    //public Color beginColor, endColor;

    public GameObject player;
    HunterCooldowns huntRef;


    void Awake()
    {
        huntRef = player.GetComponent<HunterCooldowns>();

        dashTime = huntRef.dashDuration;
        //knifeTime = huntRef.knifeDuration;

        dashUI.fillAmount = 1f;
        //knifeUI.fillAmount = 1f;
    }

    void Update()
    {
        
        if(huntRef.isDashing == true)
        {
            dashUI.fillAmount += 1.0f / dashTime * Time.deltaTime;
        }
        if(Input.GetKeyDown(huntRef.dashKey)&& huntRef.canDash) dashUI.fillAmount = 0f;
    }

}
