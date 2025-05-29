using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

//Script realizat de Craciun Claudiu-Viorel
//Script realizat de Oprea Vlad Ovidiu
public class BreakableObject : MonoBehaviour
{
    [SerializeField]
    private GameObject coin;

    [SerializeField]
    private int minCoinsDropped = 2;
    [SerializeField]
    private int maxCoinsDropped = 4;

    private Animator anim;

    private AudioSource asource;

    private int countHits = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        asource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Smash()
    {
        countHits++;
        if(countHits == 1)
        {
            asource.Play();
            anim.SetBool("smash", true);
            StartCoroutine(breakCo());
        }

    }

    IEnumerator breakCo()
    {
        yield return new WaitForSeconds(0.8f);
        this.gameObject.SetActive(false);
        int howManyCoins = Random.Range(minCoinsDropped, maxCoinsDropped);
        for(int i = 0; i < howManyCoins; i++)
        {
            Vector2 pos;
            pos.x = this.gameObject.transform.position.x + Random.Range(-0.2f, 0.2f)*5;
            pos.y = this.gameObject.transform.position.y + Random.Range(-0.2f, 0.2f)*5;
            Instantiate(coin, (Vector3)pos, Quaternion.identity);
        }
        Destroy(this.gameObject);
    }
}
