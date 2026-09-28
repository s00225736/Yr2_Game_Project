using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public enum EnemyState
{
    idle,
    Attack
}

public class Enemy : CharactersHealth
{
    GameObject loink;
    protected Rigidbody2D body;

    //state
    public CharacterState State;
    //public Sprite idleSprite;
    //public Sprite walkSprite;
    SpriteRenderer SpriteRenderer;

    //Enemy Movement
    public float movementSpeed;
    public float MinMovementSpeed = 1;
    public float MaxMovementSpeed = 3;

    //damge
    public int damage = 10;
    public float knockbackForce = 5;
    public float AttackRange = 3;

    private void Start()
    {
        loink = GameObject.FindGameObjectWithTag("Loink");
        movementSpeed = Random.Range(MinMovementSpeed, MaxMovementSpeed);

        SpriteRenderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
        //SetState(State);
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, loink.transform.position) < AttackRange)
        {
            //SetState(CharacterState.walk);
            
            //transform.rotation = loink.transform.position - transform.position;
            //body.velocity = transform.up * movementSpeed;
        }
        else
        {
            //SetState(CharacterState.idle);
        }
    }

    /*public void SetState(CharacterState newState)
    {
        State = newState;

        if (State == CharacterState.walk)
        {
            SpriteRenderer.sprite = walkSprite;
        }
        else if (State == CharacterState.idle)
        {
            SpriteRenderer.sprite = idleSprite;
        }
    }*/

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Loink"))
        {
            if (collision.contacts.Length > 0)
            {
                ContactPoint2D contact = collision.contacts[0];
                //contact point.normal value will be between 1 and -1

                if (Vector2.Dot(contact.normal, Vector2.down) >= .9f)
                {
                    Destroy(gameObject);
                }
                else
                {
                    Rigidbody2D playerRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();
                    playerRigidbody.AddForce(-contact.normal * knockbackForce, ForceMode2D.Impulse);
                }
            }
        }
    }
}
