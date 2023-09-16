using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindReactiveObject : MonoBehaviour
{
    private GameObject windController;
    private Rigidbody2D rb;
    private void Awake()
    {
        windController = GameObject.Find("WindController");
        WindController.OnWindApply += OnWind;

        rb = GetComponent<Rigidbody2D>();
    }

    private void OnWind(Vector2 windDir)
    {
        Debug.Log(windDir);
        rb.AddForce(windDir);
    }
}
