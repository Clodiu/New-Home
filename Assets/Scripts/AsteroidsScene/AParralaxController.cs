using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script realizat de Oprea Vlad Ovidiu
public class AParralaxBack : MonoBehaviour
{
    [SerializeField]
    public float scrollSpeed = 1.0f;
    void Start()
    {
        
    }

    void Update()
    {
        gameObject.transform.position -= new Vector3(1.0f, 0) * Time.deltaTime * scrollSpeed;
        if (gameObject.transform.position.y < -50)
            gameObject.transform.position = new Vector3(50, 0);
    }
}
