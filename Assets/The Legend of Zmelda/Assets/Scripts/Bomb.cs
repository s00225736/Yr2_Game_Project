using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private Animator anim;
    public float Speed = 4;
    public int damage = 50;
    public int timeUntilRecharge = 10;
    public int bombAmount = 5;
    private int maxBombAmount = 5;


    public Vector3 launchOffSet;
    public bool Throw;

    private void Start()
    {
        //if(Throw)
        //{
        //    var direction = -transform.right + Vector3.up;
        //    GetComponent<Rigidbody2D>().AddForce(direction * Speed, ForceMode2D.Impulse);
        //}
        //transform.Translate(launchOffSet);

        //Destroy(gameObject, 5); //destroy automatically after 5 seconds
    }

    private void Update()
    {
        if (bombAmount > 0)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                anim.SetTrigger("Bomb");
            }
        }
        else
        {
            RegenerateBomb();
        }

        if (!Throw)
        {
            transform.position += -transform.right * Speed * Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }

    void RegenerateBomb()
    {
        if (bombAmount < maxBombAmount)
        {
            bombAmount += timeUntilRecharge;
        }
        if (bombAmount > maxBombAmount)
        {
            bombAmount = maxBombAmount;
        }
    }
}



