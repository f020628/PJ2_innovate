using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private Vector3 dir = Vector3.left;
    private SpriteRenderer spriteRenderer;
    bool call;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        call = true;
    }

    void Update()
    {
        if (spriteRenderer.isVisible)
        {
            YourFunction();
            //FMOD PlayOneShot
            if (call == true)
            {
                FMODUnity.RuntimeManager.PlayOneShot("event:/bird", GetComponent<Transform>().position);
                call = false;
            }
            
        }
    }

    void YourFunction()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }

       
}

