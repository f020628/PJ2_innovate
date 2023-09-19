using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public void Pop()
    {
        // Add additional logic above here.
        Destroy(gameObject);
    }
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private Vector3 dir = Vector3.up;
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
