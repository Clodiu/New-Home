using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Script realizat de Craciun Claudiu-Viorel
public class DisplayCoins : MonoBehaviour
{

    public TMP_Text message;
    // Start is called before the first frame update
    void Start()
    {
        message = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        message.SetText("Coins: " + PlayerController.coins);
    }
}
