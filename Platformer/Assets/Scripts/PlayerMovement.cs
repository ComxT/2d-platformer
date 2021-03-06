﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update

    Rigidbody2D rb;
    public float speed;
    public float speedCap;
    public float jumpForce;

    bool isGrounded;
    public Transform isGroundedCheck;
    public float checkGroundRadius;
    public LayerMask groundLayer;

    public float fallMultiplier;
    public float lowJumpMultiplier;

    public float rememberGroundedFor;
    float lastTimeGrounded;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        CheckIfGrounded();
        MarioJump();
        SpeedCap();
        SpriteMirror();
    }

    void Move()
    {

        if (rb.velocity.x != 0)
        {
            anim.SetBool("isRunning", true);

        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.velocity += 3.5f * Vector2.right * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.velocity += 3.5f * Vector2.left * speed * Time.deltaTime;

        }

    }

    void Jump()
    {

        if (rb.velocity.y != 0)
        {
            anim.SetBool("isJumping", true);
        }
        else
        {
            anim.SetBool("isJumping", false);
        }


        if (Input.GetKeyDown(KeyCode.Space) && (isGrounded || Time.time - lastTimeGrounded <= rememberGroundedFor)) 
            {

                rb.velocity = new Vector2(rb.velocity.x, jumpForce);

                anim.SetTrigger("takeOff");
 
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
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    void SpeedCap() 
    {
        float cappedXvelocity = Mathf.Min(Mathf.Abs(rb.velocity.x), speedCap) * Mathf.Sign(rb.velocity.x);
        float cappedYvelocity = rb.velocity.y;

        rb.velocity =new Vector2(cappedXvelocity, cappedYvelocity);
    }

    void SpriteMirror() 
    {
        Vector3 characterScale = transform.localScale;

        if(Input.GetAxis("Horizontal") < 0)
        {
            characterScale.x = -1;
        }
        if(Input.GetAxis("Horizontal") > 0)
        {
            characterScale.x = 1;
        }
        transform.localScale = characterScale;
    }
}
