using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Variable

    //Assignables + Reference
    [Header ("Assignables")]
    public Transform orientation;
    public float playerHeight = 2f; 
    Rigidbody rb;

    //Movement 
    [Header("Movement")]
    Vector3 moveDirection;
    public float moveSpeed;
    public float movementMultiplier = 10f;
    public float airMultiplier = 0.5f;
    float horizontalMovement;
    float verticalMovement;

    [Header("Drags")]
    public float groundDrag = 3f;
    public float AirDrag = 1.5f;

    [Header("Ground Detection")]
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask groundMask;
    [SerializeField]float groudDistance = 0.4f;
    public bool isGrounded; 

    //Sprinting
    [Header("Sprinting")]
    float walkSpeed = 6f;
    float sprintSpeed = 10f; 
    float slideSpeed = 10f; 
    float acceleration = 2f;
    bool isSprinting;


    //Keybindings
    [Header("Keybindings")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] KeyCode crouchKey = KeyCode.LeftControl;
    

    //Jumping
    [Header("Jumping")]
    public float jumpForce = 300f;
    public float gravityMultiplier = 700f;
    bool isFalling, isJumping;

    private Vector3 normalVector = Vector3.up;

    //Slopes
    Vector3 slopeMoveDirection;
    RaycastHit slopeHit;


    //weapon headbobbing
    public Transform weaponParent; 
    private Vector3 weaponParentOrigin;
    private Vector3 targetWeaponBobPosition;
    private float movementCounter;
    private float idleCounter;

    bool isAiming; 

    #endregion

    #region MonoBehaviour Callbacks

    void Awake()
    {

        rb = GetComponent<Rigidbody>();

    }

    void Start()
    {

        rb.freezeRotation = true;
        weaponParentOrigin = weaponParent.localPosition;

    }

    private void FixedUpdate()
    {

        MovePlayer();

    }

    private void Update()
    {

        //ground check, make own function soon
        isGrounded = Physics.CheckSphere(groundCheck.position, groudDistance, groundMask);
        slopeMoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);

        MyInput();
        ControlDrag();
        ControlSpeed();

        //headbob
        if(horizontalMovement == 0 && verticalMovement == 0 && !isAiming) {

            //headBob(idleCounter, xintensity, yinttensity);

            HeadBob(idleCounter, 0.009f, 0.009f, 2f);
            idleCounter += Time.deltaTime;

        }else if (!isAiming){

            HeadBob(movementCounter, 0.009f, 0.04f, 8f);
            movementCounter += 2f* Time.deltaTime;
        }

        isAiming = Input.GetMouseButton(1);

    } 
    
    #endregion

    void MyInput()
    {
        //Axes
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical"); 


        //move player 
        moveDirection = orientation.forward * verticalMovement + orientation.right * horizontalMovement;
        

        //Jump
        if(Input.GetKeyDown(jumpKey) && isGrounded)
        {
            Jump();
        }

        //crouching
        if(Input.GetKeyDown(crouchKey))
        {
            StartCrouch();
        }

    }

    #region Controller Methods

    void ControlSpeed()
    {
        //Bool to check if facing forwards
        float movingForward = Input.GetAxisRaw("Vertical");


        //for faster fall during jumping
        if(!isGrounded)
        {

            rb.AddForce(Vector3.down * Time.deltaTime * gravityMultiplier);
        }


        //Sprinting Control
        if(Input.GetKey(sprintKey) && isGrounded && movingForward > 0 && !isAiming){

            isSprinting = true;
            moveSpeed = Mathf.Lerp(moveSpeed, sprintSpeed, acceleration *Time.deltaTime);

        }else if(isAiming){

            isSprinting = false;
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, 1.0f);
            
        } else {

            isSprinting = false;
            moveSpeed = Mathf.Lerp(moveSpeed, walkSpeed, acceleration *Time.deltaTime);
        }

    }

    void ControlDrag()
    {

        if(isGrounded)
        {
            rb.drag = groundDrag;

        } else {
            rb.drag = AirDrag;
        }
        
    }
    
    private bool OnSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight/2 + 0.5f))
        {
            if (slopeHit.normal != Vector3.up)
            {
                return true;

            }else {
                return false;
            }
        }
        return false;
    }
    #endregion

    #region  Physics movement Methods

    void MovePlayer()
    {

        if(isGrounded && !OnSlope())
        {
            rb.AddForce(moveDirection.normalized *moveSpeed * movementMultiplier, ForceMode.Acceleration);
            
        }
        else if (isGrounded && OnSlope())
        {
            rb.AddForce(slopeMoveDirection.normalized *moveSpeed * movementMultiplier, ForceMode.Acceleration);
        }
        else if (!isGrounded)
        {
            rb.AddForce(moveDirection.normalized *moveSpeed * movementMultiplier * airMultiplier, ForceMode.Acceleration);
        }
        //reason we use normalize here is because in the diag cross product is greater than 1 bcs 45deg

    }

    void Jump()
    {
        if(isGrounded)
        {

            isJumping = true;
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            rb.AddForce(normalVector * jumpForce * 0.5f);

        }
    }

    void StartCrouch()
    {
        if(isSprinting)
        {
            
        }
    }


    #endregion
    
    #region Flair 


    void HeadBob (float zPosi, float xIntensity, float yIntensity, float lerpTime)
    {
        targetWeaponBobPosition = weaponParentOrigin + new Vector3(Mathf.Cos(zPosi) * xIntensity, Mathf.Sin(2 * zPosi) * yIntensity, 0);

        weaponParent.localPosition = Vector3.Lerp(weaponParent.localPosition, targetWeaponBobPosition, Time.deltaTime * lerpTime);
    }
    

    #endregion
}
