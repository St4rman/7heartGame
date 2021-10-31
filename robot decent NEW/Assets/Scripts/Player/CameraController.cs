using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Variables
    [SerializeField] private float senX = 5f;
    [SerializeField] private float senY = 5f;

    [SerializeField] Transform cam;
    [SerializeField] Transform orientation;
    [SerializeField] Transform weapon;
    

    float mouseX;
    float mouseY;

    public float multiplier = 0.1f;

    float xRotation;
    float yRotation;

    #endregion

    #region MonoBehaviour
    private void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        mouseX = Input.GetAxisRaw("Mouse X");
        mouseY = Input.GetAxisRaw("Mouse Y");

        yRotation +=  mouseX * senX * multiplier; //when we look around y we're looking around in x sand vice versa
        xRotation -=  mouseY * senY * multiplier;

        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cam.transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.transform.rotation = Quaternion.Euler(0,yRotation, 0);
        weapon.localRotation = Quaternion.Euler(xRotation, yRotation, 0);
    }
    
    #endregion
}
