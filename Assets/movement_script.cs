using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Ground Check Settings")]
    public Transform groundCheckpoint;
    public float groundCheckRadius = 0.2f;
    public LayerMask groundLayer;
    private bool isGrounded;

    [Header("Movement Settings")]
    public float moveSpeed = 8f; 
    public float jumpSpeed = 12f; 
    private Rigidbody2D rb2d;
    private float horizontalInput;
    private bool jumpRequested;


    [ Header("Smoother movements")]
    public float acceleration = 50f; 
    public float deceleration = 40f;  
    public float velPower = 0.9f;   

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
      
        if (groundCheckpoint != null)
        {
            isGrounded = Physics2D.OverlapCircle(groundCheckpoint.position, groundCheckRadius, groundLayer);
        }

       
        horizontalInput = Input.GetAxisRaw("Horizontal");

       
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequested = true;
        }

        isGrounded = Physics2D.OverlapCircle(groundCheckpoint.position, groundCheckRadius, groundLayer);

         horizontalInput = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequested = true;
        }
    }

    void FixedUpdate()
    {
       
        if (isGrounded)
        {
            float targetspeed = horizontalInput * moveSpeed;
            float accelrate = (Mathf.Abs(targetspeed) > 0.01) ? acceleration : deceleration;
    float newVelX = Mathf.MoveTowards(rb2d.linearVelocity.x, targetspeed, accelrate * Time.fixedDeltaTime);
            rb2d.linearVelocity = new Vector2(newVelX, rb2d.linearVelocity.y);
        }
        else
        {
            
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, rb2d.linearVelocity.y);
        }

     
        if (jumpRequested)
        {
            rb2d.linearVelocity = new Vector2(rb2d.linearVelocity.x, jumpSpeed);
            jumpRequested = false; 
        }
    }

    
    
}