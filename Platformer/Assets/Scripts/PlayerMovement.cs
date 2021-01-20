using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D rb;
    public float speed;
    public float jumpForce;

    bool isGrounded;
    public Transform isGroundedCheck;
    public float checkGroundRadius;
    public LayerMask groundLayer;

    public float fallMultiplier;
    public float lowJumpMultiplier;

    public float rememberGroundedFor;
    float lastTimeGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        CheckIfGrounded();
        MarioJump();
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor)) 
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
    }

    void CheckIfGrounded() 
    {
        Collider2D collider = Physics2D.OverlapCircle(isGroundedCheck.position, checkGroundRadius, groundLayer);

        if (collider != null)
        {
            isGrounded = true;
        }
        else if (collider != null)
        {
            lastTimeGrounded = Time.time - rememberGroundedFor;
            isGrounded = false;
        }
        else
            isGrounded = false;

    }

    void MarioJump()
    {
        if (rb.velocity.y < 0)
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}
