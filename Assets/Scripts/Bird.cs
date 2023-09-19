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
            // �������������Ұ�ڣ�ִ����ĺ���
            YourFunction();
        }
    }

    void YourFunction()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }

       
}

