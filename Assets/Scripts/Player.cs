using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed = 10;
    public float jumpHeight = 3;
    public float dashSpeed = 30;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;


    [Header("Jump")]
    public Transform groundCheck; //player legs
    public LayerMask groundLayer;
    public float groundCheckRadius = 0.2f;
    public int maxJumps = 1;


    [Header("Jump Mechanics")]
    public float coyoteTime = 0.3f;
    public float jumpBufferTime = 0.2f;

    private float jumpBufferCounter;
    private float coyoteTimeCounter;
    private bool isGrounded;
    private Rigidbody2D rb;
    private float inputX;
    private int jumpsLeft;
    private bool isDashing;
    private float dashTime;
    private float dashCooldownTime;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime;
            jumpsLeft = maxJumps;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if ((coyoteTimeCounter > 0 || jumpsLeft > 0) && jumpBufferCounter > 0)
        {
            jumpBufferCounter = 0; //prevent infinite jump

            var jumpVelocity = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y * rb.gravityScale);

            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);

            if (!isGrounded)
            {
                jumpsLeft--;
            }
        }

        //dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTime <= 0)
        {
            isDashing = true;
            dashTime = dashDuration;
            dashCooldownTime = dashCooldown;
        }

        if (isDashing)
        {
            rb.velocity = new Vector2(inputX * dashSpeed, rb.velocity.y);
            dashTime -= Time.deltaTime;

            if (dashTime <= 0)
            {
                isDashing = false;
            }
        }
        dashCooldownTime -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(inputX * movementSpeed, rb.velocity.y);
    }
}
