using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public float speed;
    public float attackRange;
    public int attackDamage;
    public int maxHealth = 500;
    private int currentHealth;

    private Transform loink;
    private Rigidbody2D body;
    public Animator animator;

    public float spinAttackDuration;
    public float spinAttackSpeed;
    public float slamAttackDuration;
    public float slamAttackDelay;
    public float slamAttackRadius;
    public int slamAttackDamage;
    private bool isAttacking;
    private float spinAttackDelay;

    private bool isDead = false;

    public bool isPhaseTwo = false;
    public GameObject phaseTwoObject;


    void Start()
    {
        loink = GameObject.FindGameObjectWithTag("Loink").transform;
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (isDead) return;

        if (!isAttacking)
        { 
        if (transform.position.x < loink.position.x)
        {
            body.velocity = new Vector2(speed, body.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        else
        {
            body.velocity = new Vector2(-speed, body.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }

        if (Vector2.Distance(transform.position, loink.position) < attackRange)
        {
            animator.SetBool("Attack", true);
        }
        else
        {
            animator.SetBool("Attack", false);
        }
       }
    }

    void ActivatePhaseTwo()
    {
        isPhaseTwo = true;
        currentHealth = maxHealth;
        phaseTwoObject.SetActive(true);
        gameObject.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        animator.SetBool("Hurt", true);
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            animator.SetBool("Hurt", false);
        }

         if (!isPhaseTwo && currentHealth <= maxHealth / 2)
        {
            ActivatePhaseTwo();
        }
        else
        {
            animator.SetBool("Hurt", true);
        }
    }

    void Die()
    {
        animator.SetInteger("Die", 0);
        Destroy(gameObject);
    }

    // Function to deal damage to the player on attack
    void Attack()
    {
        if (Vector2.Distance(transform.position, loink.position) < attackRange)
        {
            loink.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
            animator.SetBool("Attack", true);
        }
        else
        {
            animator.SetBool("Attack", false);
        }
    }

    // Function for spin attack
    void SpinAttack()
    {
        StartCoroutine(DoSpinAttack());
    }

    // Coroutine for spin attack
    IEnumerator DoSpinAttack()
    {
        isAttacking = true;
        body.velocity = Vector2.zero;
        animator.SetBool("Spin", true);
        yield return new WaitForSeconds(spinAttackDelay);
        body.velocity = new Vector2(transform.localScale.x * spinAttackSpeed, 0);
        yield return new WaitForSeconds(spinAttackDuration);
        body.velocity = Vector2.zero;
        animator.SetBool("Spin", false);
        isAttacking = false;
    }

    // Function for slam attack
    void SlamAttack()
    {
        StartCoroutine(DoSlamAttack());
    }

    // Coroutine for slam attack
    IEnumerator DoSlamAttack()
    {
        isAttacking = true;
        body.velocity = Vector2.zero;
        animator.SetTrigger("slamAttackStart");
        yield return new WaitForSeconds(slamAttackDelay);
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, slamAttackRadius);
        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("Loink"))
            {
                hit.GetComponent<PlayerHealth>().TakeDamage(slamAttackDamage);
            }
        }
        yield return new WaitForSeconds(slamAttackDuration - slamAttackDelay);
        animator.SetBool("Slam", true);
        isAttacking = false;

        if (!isPhaseTwo && currentHealth <= maxHealth / 2)
        {
            ActivatePhaseTwo();
        }
    }

    // Draw gizmos for slam attack radius
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, slamAttackRadius);
    }

    
}

