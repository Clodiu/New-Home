using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Script realizat de Craciun Claudiu-Viorel
public class Mutulica : MonoBehaviour
{
    [SerializeField]
    private int minimumCoins = 500;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(PlayerController.coins >= minimumCoins)
            {
                SceneManager.LoadScene(sceneName: "Ending");
            }
        }
    }
}
