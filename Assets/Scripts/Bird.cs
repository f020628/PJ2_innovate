using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private Vector3 dir = Vector3.left;

    void Update()
    {
        transform.Translate(dir * speed * Time.deltaTime);
    }
}
