using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script realizat de Craciun Claudiu-Viorel
public class EnemyHit : MonoBehaviour
{
    [SerializeField]
    private float hitSpeed = 1;
    [SerializeField]
    private float enemyDamage = 1;

    private GameObject player;

    private PlayerController playerController;

    private bool isClose = false;

    private float lastHitTime;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (ShouldHit())
        {
            Hit();
        }
    }

    private bool ShouldHit()
    {
        if (isClose)
        {
            var currentTime = Time.time;
            if ((currentTime - lastHitTime) >= hitSpeed)
            {
                lastHitTime = currentTime;
                return true;
            }
        }
        return false;
    }

    private void Hit()
    {
        playerController.takeDamage(enemyDamage);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            lastHitTime = Time.time;
            isClose = true;
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            lastHitTime = Time.time;
            isClose = false;
        }
    }
}
