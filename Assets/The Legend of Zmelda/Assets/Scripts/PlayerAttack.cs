using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerAttack : PlayerState
{
    //sword variables
    [SerializeField] private Animator anim;
    public Animator anime;
    private float meleeSpeed;
    public int damage;
    public float timeUntilMelee;

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (timeUntilMelee <= 0f)
        {
            if (Input.GetMouseButtonDown(0))
            {
                anime.SetBool("IsAttacking", true);

                timeUntilMelee = meleeSpeed;
            }
            else
                anime.SetBool("IsAttacking", false);
        }
        else
        {
            //SetState(CharacterState.walk);
            
            timeUntilMelee -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }
}
