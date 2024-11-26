using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed = 10;
    public float jumpHeight = 3;

    [Header("Jump")]
    public Transform groundCheck; //legs 
    public LayerMask groundLayer;
    public float groundCheckRadius;


    private bool isGrounded;
    private Rigidbody2D rb;
    private float inputX;





    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        inputX = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);


        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            var jumpVelocity = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y * rb.gravityScale);

            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
        }

    }

    private void FixedUpdate() 
    {
        rb.velocity = new Vector2(inputX * movementSpeed, rb.velocity.y);
    }


}
