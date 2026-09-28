using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersHealth : MonoBehaviour
{
    public int Health = 100;
    public int MaxHealth = 100;
    public Animator anim;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollision(collision.gameObject);
    }

    public void AddHealth(int amount)
    {
        amount = 0;
        amount += Health;

        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
    }

    public void SubtractHealth(int amount)
    {
        Health -= amount;

        if (Health <= 0)
        {
            OnDeath();
        }
    }

    public virtual void OnDeath()
    {
        //Destroy(gameObject);
    }

    public virtual void HandleCollision(GameObject otherObject) 
    {

    }
}
