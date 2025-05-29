using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Script realizat de Craciun Claudiu-Viorel
public class PlanetHit : MonoBehaviour
{
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
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Banutii playerului"+ PlayerController.coins);
            if (this.gameObject.CompareTag("ApocalypticTerra"))
            {
                TargetPlanet.PlanetName = "ApocalypticTerra";
                SceneManager.LoadScene(sceneName: "ApocalypticTerra");
            }
            else if (this.gameObject.CompareTag("Planet1"))
            {
                TargetPlanet.PlanetName = "Planet1";
                SceneManager.LoadScene(sceneName: "Asteroids");
            }
            else if (this.gameObject.CompareTag("Planet2"))
            {
                TargetPlanet.PlanetName = "Planet2";
                SceneManager.LoadScene(sceneName: "Asteroids");
            }
            else if (this.gameObject.CompareTag("PlanetX"))
            {
                TargetPlanet.PlanetName = "PlanetX";
                SceneManager.LoadScene(sceneName: "PlanetX");
            }
        }
    }
}
