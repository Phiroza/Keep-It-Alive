using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 200;
    private float movement;
    private bool isFacingRight = true;

    private bool isJumping;
    public float jumpForce = 100;
    public float jumpTimer = 1f;
    private float jumpTimerCounter;
    private bool justOnceForce;

    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask groundLayer;

    private bool isDashing;
    public float dashTimer;
    //[SerializeField]
    private float dashTimerCounter;
    public float dashSpeed;
    public float dashCooldownTimer;
    [SerializeField]
    private float dashCooldownTimerCounter;
    private float fixedMovement;
    private float originalGravityScale;

    private float timerWalkSound = 0.15f;
    private float timerWalkSoundCounter;


    private Rigidbody2D rb;
    private Animator anim;
    private TrailRenderer trailRenderer;

    void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.enabled = false;
        originalGravityScale = rb.gravityScale;

        dashCooldownTimerCounter = 0;
    }

    //======================================================UPDATE======================================================
    void Update()
    {
        // -----------------------------------------INPUT MOVEMENT + ANIM
        movement = Input.GetAxisRaw("Horizontal");

        if (movement != 0) { anim.SetBool("isRunningAnim", true); PlaySoundWalk(); }
        else anim.SetBool("isRunningAnim", false);

        // -----------------------------------------CHECK RIGHT-LEFT FACING
        if (movement < 0 && isFacingRight == true)
        {
            Flip();
            isFacingRight = false;
        }
        else if (movement > 0 && isFacingRight == false)
        {            
            Flip();
            isFacingRight = true;            
        }

        // ---------------------------------------CHECK IF GROUNDED + ANIM
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, groundLayer);
        if (!isGrounded) anim.SetBool("isJumpingAnim", true);
        else anim.SetBool("isJumpingAnim", false);


        // ----------------------------------------CHECK IF JUMP PRESSED
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) 
            && isGrounded) 
        {
            isJumping = true;
            jumpTimerCounter = jumpTimer; 
        }

        jumpTimerCounter -= Time.deltaTime;

        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)) 
            || jumpTimerCounter < 0)
        {
            isJumping = false;
            justOnceForce = false;
        }

        // ----------------------------CHECK IF DASHING + COOLDOWN timer + ANIM
        if ((Input.GetKeyDown(KeyCode.RightShift)||Input.GetMouseButtonDown(1) )&& dashCooldownTimerCounter <= 0)
        {
            if (isFacingRight)
                fixedMovement = 1;
            else if (isFacingRight==false)
                fixedMovement = -1;

            isDashing = true;
            trailRenderer.enabled = true;
            anim.SetBool("isDashingAnim", true);
            FindObjectOfType<AudioManager>().Play("Dash");

            dashCooldownTimerCounter = dashCooldownTimer;
        } else if (dashCooldownTimer > 0)
        {
            dashCooldownTimerCounter -= Time.deltaTime;
        }

        // ------------------------------------------DASH timer + ANIM
        if (isDashing)
        {
            dashTimerCounter -= Time.deltaTime;
            if (dashTimerCounter <= 0)
            {
                isDashing = false;
                anim.SetBool("isDashingAnim", false);
                trailRenderer.enabled = false;

                dashTimerCounter = dashTimer;

                rb.gravityScale = originalGravityScale;
            }
        }
    }

    //===================================================FIXED-UPDATE===================================================
    private void FixedUpdate()
    {
        Move();  

        Jump();

        if (isDashing)
            Dash();
    }

    void Move()
    {        
        rb.velocity = new Vector2(movement * speed * Time.fixedDeltaTime * 10, rb.velocity.y);
    }

    void Flip()
    {
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }

    void Jump()
    {
        if (isJumping)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.fixedDeltaTime * 10);
            if (justOnceForce == false)
            {
                rb.AddForce(Vector2.up * jumpForce * 0.01f, ForceMode2D.Impulse);
                justOnceForce = true;
            }
    }

    void Dash()
    {        
        rb.velocity = new Vector2 ( fixedMovement * dashSpeed * Time.fixedDeltaTime * 10,0);
        rb.gravityScale = 0;
    }

    void PlaySoundWalk()
    {
        if (timerWalkSoundCounter <= 0 && isGrounded)
        {
            timerWalkSoundCounter = timerWalkSound;
            FindObjectOfType<AudioManager>().Play("Walk");
        }
        else timerWalkSoundCounter -= Time.deltaTime;
    }
}
