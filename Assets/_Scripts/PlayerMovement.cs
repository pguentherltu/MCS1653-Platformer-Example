using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public float jumpForce = 12.0f;
    public BoxCollider2D groundCollider;
    public Animator animator;

    private Rigidbody2D rb;
    private const float gravity = 2.0f;
    private SpriteRenderer spriteRenderer;

    // Improvements to consider:
    // - Double jump
    // - Easing into movement (accelerating more slowly)

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = gravity;
        animator.Play("player_idle");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
            spriteRenderer.flipX = true;
            animator.Play("player-running");
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            spriteRenderer.flipX = false;
            animator.Play("player-running");
        }

        if (IsGrounded())
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode2D.Impulse);
                animator.Play("player_jumping");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.transform.CompareTag("Elevator"))
        {
            transform.SetParent(collision.gameObject.transform, true); // true = keep world position
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Elevator"))
        {
            transform.parent = null; //could also SetParent(null)
        }
    }

    private bool IsGrounded()
    {
         return groundCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
    }
}
