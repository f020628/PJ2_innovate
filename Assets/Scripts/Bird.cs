using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private Vector3 dir = Vector3.left;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (spriteRenderer.isVisible)
        {
            // 物体在摄像机视野内，执行你的函数
            YourFunction();
        }
    }

    void YourFunction()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }

       
}

