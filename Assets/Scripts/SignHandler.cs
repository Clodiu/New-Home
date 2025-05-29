using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//Script realizat de Oprea Vlad Ovidiu
public class SignHandler : MonoBehaviour
{
    [SerializeField]
    public GameObject player;

    [SerializeField]
    public GameObject signInteractPrompt;

    [SerializeField]
    public GameObject textbox;

    [SerializeField]
    public TextMeshProUGUI textboxContent;

    [SerializeField]
    public string textboxString;

    public bool isTextboxOpen = false;

    private float MapClamp(float x, float in_min, float in_max, float out_min, float out_max)
    {
        if (x < in_min)
            return out_min;

        if (x > in_max)
            return out_max;
        
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }

    void Start()
    {
        
    }

    void Update()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        signInteractPrompt.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, this.MapClamp(dist, 2f, 3f, 1f, 0f));

        bool old_isTextboxOpen = isTextboxOpen;

        if(dist < 3.0f && Input.GetKeyDown(KeyCode.E))
            isTextboxOpen = !isTextboxOpen;

        if (dist > 3.0f)
            isTextboxOpen = false;

        if(old_isTextboxOpen != isTextboxOpen)
        {
            if (old_isTextboxOpen)
                textbox.SetActive(false);
        }

        if(isTextboxOpen)
        {
            textbox.SetActive(true);
            textboxContent.text = textboxString;
        }
    }
}
