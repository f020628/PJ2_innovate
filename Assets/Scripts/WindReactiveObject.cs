using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindReactiveObject : MonoBehaviour
{
    private GameObject windController;
    public PaperPlane plane;
    private float cd = 1;
    private float time = 2;
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
        if(plane.slider.value >= 0.05f && time > cd)
        {
            plane.ReceiveWind(windDir);
        }
        else if(time < cd)
        {
            time += Time.deltaTime;
        }
        else if(plane.slider.value < 0.05f)
        {
            time = 0;
        }
        
        plane.slider.value += Time.deltaTime * plane.regeneration;
        
    }
}
