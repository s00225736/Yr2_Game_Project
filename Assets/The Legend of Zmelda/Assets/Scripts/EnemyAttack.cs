using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    GameObject loink;
    protected Rigidbody2D body;

    //sword variables
    [SerializeField] private Animator anim;

    public float movementSpeed;
    public float MinMovementSpeed = 1;
    public float MaxMovementSpeed = 3;

    private float meleeSpeed;

    public int damage = 10;
    public float AttackRange = 3;


    public string PlayerTag = "Loink";
    void Start()
    {
        //finding the player
        loink = GameObject.FindGameObjectWithTag(PlayerTag);
        movementSpeed = Random.Range(MinMovementSpeed, MaxMovementSpeed);

    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, loink.transform.position) < AttackRange)
        {
            transform.up = loink.transform.position - transform.position;
            body.velocity = transform.up * movementSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Loink")
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
        }
    }

}
