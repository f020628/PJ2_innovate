using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindReactiveObject : MonoBehaviour
{
    private GameObject windController;
    public PaperPlane plane;
    private void Awake()
    {
        windController = GameObject.Find("WindController");
        WindController.OnWindApply += OnWind;
        
    }

    private void Start()
    {
        //plane = GetComponent<PaperPlane>();
    }
    private void OnWind(Vector2 windDir)
    {

        plane.ReceiveWind(windDir);
    }
}
