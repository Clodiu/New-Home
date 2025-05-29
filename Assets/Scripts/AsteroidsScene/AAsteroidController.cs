using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script realizat de Oprea Vlad Ovidiu
public class AAsteroidController : MonoBehaviour
{
    private BoxCollider2D bc;
    private Rigidbody2D rb;
    private Animator animator;
    void Start()
    {
        gameObject.tag = "Asteroid";
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(Random.Range(-0.002f, -0.008f), 0));
        rb.AddTorque(Random.Range(3f, 15f));
        animator = gameObject.GetComponent<Animator>();
        bc = gameObject.GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        if (rb.position.x < -8)
            animator.SetBool("IsExploding", true);
    }

    void FixedUpdate()
    {

    }

    public IEnumerator DestroyAsteroid()
    {
        Destroy(gameObject);
        yield return null;

    }

    public IEnumerator PlaySfxAsteroid()
    {
        GetComponent<AudioSource>().Play(); 
        yield return null;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Asteroid" || collision.gameObject.tag == "PlayerOOBLiimt")
        {
            Physics2D.IgnoreCollision(bc, collision);
        } else
        {
            animator.SetBool("IsExploding", true);
        }
    }
}
