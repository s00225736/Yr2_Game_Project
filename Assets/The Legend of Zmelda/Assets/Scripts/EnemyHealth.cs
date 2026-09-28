using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : CharactersHealth
{
    Arrow arrow;
    int damage;

    public override void HandleCollision(GameObject otherObject)
    {
        if (otherObject.CompareTag("Arrow"))
        {
            arrow = otherObject.GetComponent<Arrow>();
            SubtractHealth(arrow.damage);
        }
        base.HandleCollision(otherObject);

        if (otherObject.CompareTag("Sword"))
        {
            SubtractHealth(damage);
        }
        //base.HandleCollision(otherObject);
    }

    public void TakeDamage(int Damage)
    {
        Health -= Damage;

        if (Health <= 0f)
        {
            Destroy(gameObject);
        }
    }

    public override void OnDeath()
    {
        Destroy(gameObject);

        base.OnDeath();
    }
}
