using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script realizat de Oprea Vlad Ovidiu
public class ALaserShot : MonoBehaviour
{
    private Rigidbody2D rb;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(0.35f, 0));
    }

    void Update()
    {
        if (rb.position.x > 200)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
