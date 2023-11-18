using System;
using System.Collections;
using System.Collections.Generic;
//using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float jumpSpeed = 10f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] CompositeCollider2D groundCollider;
    [SerializeField] Vector2 deadKick = new Vector2(10f, 10f);
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;

    GameObject tmpBullet;
    Vector2 moveInput;
    Rigidbody2D rb;
    Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    float originalGravity;
    bool isAlive = true;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        originalGravity = rb.gravityScale;
    }

    void Update()
    {
        if (!isAlive) return;
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
    }

    void OnFire(InputValue value)
    {
        if (!isAlive) return;

        tmpBullet = Instantiate(bullet, gun.position, transform.rotation);
        //tmpBullet.SetActive(true);
    }

    void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies", "Hazards")))
        {
            isAlive = false;
            GetComponent<SpriteRenderer>().color = Color.red;
            rb.velocity = deadKick;
            myAnimator.SetTrigger("Dead");
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
        }
    }

    void FlipSprite()
    {
        bool isPlayerMoving = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;

        if(isPlayerMoving)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.velocity.x), 1f);
        }
    }

    void Run()
    {
        Vector2 speed = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
        rb.velocity = speed;

        bool isPlayerMoving = Mathf.Abs(rb.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", isPlayerMoving);
    }

    void OnMove(InputValue value)
    {
        if (!isAlive) return;
        moveInput = value.Get<Vector2>();
    }
    void OnJump(InputValue value)
    {
        if (!isAlive) return;
        bool canJump = myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
        if(canJump)
        {
            rb.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    void ClimbLadder()
    {
        bool canClimb = myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"));
        if (canClimb)
        {
            bool climbing = Mathf.Abs(rb.velocity.y) > Mathf.Epsilon;
            myAnimator.SetBool("isClimbing", climbing);
            rb.gravityScale = 0;
            rb.velocity = new Vector2(rb.velocity.x, moveInput.y * climbSpeed);
        }
        else
        {
            myAnimator.SetBool("isClimbing", false);
            rb.gravityScale = originalGravity;
        }

        //ba nardebban barkhord dare bad k fasele migire roshan mishe ground baz barkhord
        //dare khmush mishe va vase hamin vaqti yeho bere jolo miyofte + ba zamin ham miyofte
        //chun ba nardeban barkhord dare
        //if (Mathf.Abs(moveInput.y) > Mathf.Epsilon)
        //{
        //    if(canClimb)
        //    {
        //        rb.velocity = new Vector2(rb.velocity.x, moveInput.y * climbSpeed);
        //        groundCollider.enabled = false;
        //    }
        //}
        //else
        //{
        //    groundCollider.enabled = true;
        //}
    }
}

