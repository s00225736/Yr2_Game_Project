using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int Value;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Loink"))
        {
            Collider2D collider = GetComponent<Collider2D>();
            collider.enabled = false;

            Remove();
        }
    }

    public void Remove()
    {
        Destroy(gameObject);
    }
}
