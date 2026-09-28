using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Color ActivatedColor;

    private void onTrigger(Collision2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Loink"))
        {
            GetComponent<SpriteRenderer>().color = ActivatedColor;
            GetComponent<SpriteRenderer>().flipX = true;

        }
    }
}
