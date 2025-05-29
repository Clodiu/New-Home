using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Rendering;

//Script realizat de Craciun Claudiu-Viorel
public class PlayerHit : MonoBehaviour
{
    private float playerDamage = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController e;
            e = other.GetComponent<EnemyController>();
            e.takeDamage(playerDamage);
        }
        if (other.CompareTag("BreakableObject"))
        {
            other.GetComponent<BreakableObject>().Smash();
        }
    }
}
