using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script realizat de Oprea Vlad Ovidiu
public class APlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField]
    private float moveSpeed = 5.0f;

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private KeyCode attackKey;

    // Update <--> FixedUpdate
    private Vector2 input;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(Input.GetKeyDown(attackKey))
        {
            GameObject bullet = Instantiate(bulletPrefab, gameObject.transform.position + new Vector3(0.5f, 0), Quaternion.identity);
            // Debug.Break();
        }
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + (input.normalized * moveSpeed * Time.fixedDeltaTime));
    }
}
