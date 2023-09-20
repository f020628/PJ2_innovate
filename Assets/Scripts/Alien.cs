using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : MonoBehaviour
{
     public float speed = 2.0f;
    public float distance = 4.0f;
    private Vector3 startPos;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPos = transform.position;
    }

    void Update()
    {
        if (spriteRenderer.isVisible)
        {
            Vector3 newPos = startPos + transform.right * Mathf.PingPong(Time.time * speed, distance);
            transform.position = newPos;
        }
        
    }
}
