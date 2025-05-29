using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script realizat de Craciun Claudiu-Viorel
public enum EnemyState
{
    walk,
    attack,
}

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private GameObject coin;

    [SerializeField]
    private int minCoinsDropped = 2;
    [SerializeField] 
    private int maxCoinsDropped = 4;

    public EnemyState currentState;

    private Animator animator;


    private GameObject player;
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float maxSeeDistance = 5;
    [SerializeField]
    private float minSeeDistance = 1;

    [SerializeField]
    private float health = 5f;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        //Debug.Log(direction);
        if (distance < maxSeeDistance && distance > minSeeDistance)
        {
            animator.SetFloat("moveX", direction.x);
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    public void takeDamage(float amount)
    {
        this.health = this.health - amount;
        if (this.health <= 0)
        {
            this.health = 0;
            int howManyCoins = Random.Range(minCoinsDropped, maxCoinsDropped);
            for (int i = 0; i < howManyCoins; i++)
            {
                Vector2 pos;
                pos.x = this.gameObject.transform.position.x + Random.Range(-0.2f, 0.2f)*5;
                pos.y = this.gameObject.transform.position.y + Random.Range(-0.2f, 0.2f)*5;
                Instantiate(coin, (Vector3)pos, Quaternion.identity);
            }
            Destroy(gameObject);
        }
    }
}
