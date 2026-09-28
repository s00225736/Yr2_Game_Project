using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loink: MonoBehaviour
{
    public SpriteRenderer SpriteRenderer;

    protected Rigidbody2D body;

    Enemy enemy;

    public Animator anim;
    public float MovementSpeed;
    public float AirMovementSpeed;

    #region Old COde
    public float JumpForce;

    float horizontal;
    Vector2 horizontalForce;
    Vector2 verticalForce;
    public bool isOnGround;

    public float MaxSlope = 0.5f;
    public int MaxJumps = 2;
    int currentJumps;

    public Vector2 checkpointPosition;
    public int CollectableCount;
    #endregion

    public float MaxMovementSpeed = 10;

    public int collectables;

    private void Start()
    {
        verticalForce.y = JumpForce;
        //SpriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        anim.SetFloat("Speed", Mathf.Abs(horizontal));
        if (isOnGround)
            horizontalForce.x = horizontal * MovementSpeed * Time.deltaTime;
        else
            horizontalForce.x = horizontal * AirMovementSpeed * Time.deltaTime;

        body.AddForce(horizontalForce);

        
        if (Input.GetKeyDown(KeyCode.Space) && CanJump())
        {
            body.AddForce(verticalForce, ForceMode2D.Impulse);
            isOnGround = false;
            currentJumps++;
        }

        if (horizontal > 0)
        {
            SpriteRenderer.flipX = true;
        }
        else if (horizontal < 0)
        {
            SpriteRenderer.flipX = false;
        }

        //Limit the velocity of the player to be MaxMovementSpeed
        body.velocity = Vector2.ClampMagnitude(body.velocity, MaxMovementSpeed);
    }

    bool CanJump()
    {
        return isOnGround || currentJumps < MaxJumps;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckIfOnGround(collision);
    }

    void CheckIfOnGround(Collision2D collision)
    {
        if (!isOnGround)
            if (collision.contacts.Length > 0)
            {
                ContactPoint2D contact = collision.contacts[0];
                //how close does the normal match the up direction
                float dot = Vector2.Dot(contact.normal, Vector2.up);
                isOnGround = dot >= MaxSlope;

                if (isOnGround)
                    currentJumps = 0;
            }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("CheckPoint"))
        {
            checkpointPosition = collision.gameObject.transform.position;
        }

        else if (collision.gameObject.CompareTag("Death"))
        {
            body.velocity = Vector2.zero;
            transform.position = checkpointPosition;
        }
        else if (collision.gameObject.CompareTag("Collectable"))
        {
            collectables++;
        }
    }

    public void OnDeath()
    {
        body.velocity = Vector2.zero;
        transform.position = checkpointPosition;
    }
}
