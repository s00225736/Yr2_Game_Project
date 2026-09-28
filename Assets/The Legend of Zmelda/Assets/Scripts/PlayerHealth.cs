using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : CharactersHealth
{
    Enemy enemy;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision.gameObject);
    }

    public override void HandleCollision(GameObject otherObject)
    {
        if (otherObject.CompareTag("Enemy"))
        {
            enemy = otherObject.GetComponent<Enemy>();
            SubtractHealth(enemy.damage);
            anim.SetTrigger("Hurt");
        }
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
        anim.SetTrigger("Die");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}


