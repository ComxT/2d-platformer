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
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) 
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
    }

    void CheckIfGrounded() 
    {
        Collider2D collider = Physics2D.OverlapCircle(isGroundedCheck.position, checkGroundRadius, groundLayer);
            
        if(collider != null) 
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }

    }
}
