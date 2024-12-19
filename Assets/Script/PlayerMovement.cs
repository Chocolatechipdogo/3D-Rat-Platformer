using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variables for Movement 
    public float moveSpeed;
    public Transform orientation;

    private float horizontalInput;
    private float verticalInput;

    private Vector3 moveDirection;

    private Rigidbody rb;

    //varibles for jump
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    private bool readyToJump;
    public KeyCode jumpKey = KeyCode.Space;

    // Variables for checking for Grounded
    public float playerHeight;
    public float groundDrag;
    public LayerMask isGround;
    bool grounded;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    
    //If anything rigidBody for movement add here
    void FixedUpdate()
    {
        PlayerMove();
    }

    // Update is called once per frame
    void Update()
    {
        //to check if on ground
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, isGround);

        PlayerInput();
        SpeedControl();

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else 
        { 
            rb.drag = 0;
        }

        //movement
        /*
        MyInput();
        SpeedControl();
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 5.0f, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 5.0f * airMultiplier, ForceMode.Force);
        }
       */





    }

    private void PlayerInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        

        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Debug.Log("Entered the if to jump");
            readyToJump = false;

            Jump();

            Invoke(nameof(RestJump), jumpCooldown);
        }


    }

    private void PlayerMove()
    {
        
        //SpeedControl();
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 5.0f, ForceMode.Force);
        }
        else if (!grounded)
        {
            rb.AddForce(moveDirection.normalized * moveSpeed * 5.0f * airMultiplier, ForceMode.Force);
                      
        }
    }

    private void SpeedControl() 
    {
        //current velocity
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        //limit velocity
        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 0f);

        rb.AddForce(-transform.forward * jumpForce, ForceMode.Impulse);
        Debug.Log(readyToJump);
    }

    private void RestJump()
    {
        readyToJump = true;
    }
   
}
