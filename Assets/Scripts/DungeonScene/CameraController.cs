using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script realizat de Craciun Claudiu-Viorel
//Script realizat de Oprea Vlad Ovidiu
public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform target;   // Ce entitate urmarim.
    [SerializeField]
    private float smoothing;    // Cat de "neteda" este miscarea catre target.

    private void Start()
    {
        // Camera trebuie sa inceapa centrata pe jucator.
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y, transform.position.z);
    }

    void FixedUpdate()
    {
        if (transform.position != target.position) {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
    }
}
