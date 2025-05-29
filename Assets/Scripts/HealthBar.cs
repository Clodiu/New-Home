using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Script realizat de Craciun Claudiu-Viorel
public class HealthBar : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider=GetComponent<Slider>();
        slider.value = player.GetComponent<PlayerController>().maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = player.GetComponent<PlayerController>().health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
}
