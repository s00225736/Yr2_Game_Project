using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Level1End : MonoBehaviour
{
    BoxCollider2D boxer;

    // Start is called before the first frame update
    void Start()
    {
        boxer = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Loink"))
            SceneManager.LoadScene("Level2");
    }
}
