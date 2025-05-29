using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script realizat de Craciun Claudiu-Viorel
//Script realizat de Oprea Vlad Ovidiu
public class Coin : MonoBehaviour
{
    [SerializeField]
    private int value = 1;
    private AudioSource asource;
    // Start is called before the first frame update
    void Start()
    {
        asource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Ai luat un banut");
            StartCoroutine(PlayCoinCo(other));
        }
    }

    private IEnumerator PlayCoinCo(Collider2D other)
    {
        asource.Play();
        GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(0.21f);
        other.GetComponent<PlayerController>().addCoin(value);
        Destroy(this.gameObject);
    }
}
