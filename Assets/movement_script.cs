using UnityEngine;

public class SnappyPlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 12f;
    public float sprintSpeed = 20f;
    public float acceleration = 90f;
    public float deceleration = 60f;

    [Header("Jump Settings")]
    public float jumpForce = 16f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb2d;
    private float horizontalInput;
    private bool isSprinting;
    private bool isGrounded;
    private bool jumpRequested;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
       
        horizontalInput = Input.GetAxisRaw("Horizontal");
        isSprinting = Input.GetKey(KeyCode.LeftShift); 

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequested = true;
        }
    }
    private void HandleMovement()
    {
        
        float currentTargetSpeed = isSprinting ? sprintSpeed : moveSpeed;
        float targetVelocityX = horizontalInput * currentTargetSpeed;

        
        float rate = (Mathf.Abs(targetVelocityX) > 0.01f) ? acceleration : deceleration;

        
        float newVelocityX = Mathf.MoveTowards(rb2d.linearVelocity.x, targetVelocityX, rate * Time.fixedDeltaTime);
        rb2d.linearVelocity = new Vector2(newVelocityX, rb2d.linearVelocity.y);
    }
    private void HandleJumping()
    {
       
        if (jumpRequested)
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpForce);
            jumpRequested = false;
        }

        if (rb2d.linearVelocity.y < 0)
        {
            rb2d.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
       
        else if (rb2d.linearVelocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb2d.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }
    }





    private void FixedUpdate()
    {
        
        if (groundCheck != null)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        }

      
        HandleMovement();
        HandleJumping();
    }

   

    
    }

 
    
