using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindReactiveObject : MonoBehaviour
{
    public PaperPlane plane;
    private float cd = 0.6f;
    private float time = 2;
    private bool ok = true;
    private void Awake()
    {
        WindController.OnWindApply += OnWind;
    }

    private void Start()
    {
        //plane = GetComponent<PaperPlane>();
    }
    private void OnWind(Vector2 windDir)
    {
        if(plane.slider.value > 0.05f && windDir != Vector2.zero)
        {
            plane.ReceiveWind(windDir);
        }
        else if(plane.slider.value <= 0.05f && windDir != Vector2.zero)
        {
            time = 0;
            ok = false;
        }

        if(ok)
        {
            plane.slider.value += Time.deltaTime * plane.regeneration;
        }
        else if(time <= cd && !ok)
        {    
            time += Time.deltaTime;
        }
        else if(time > cd && !ok)
        {
            ok = true;
        }
    }

    private void OnDisable()
    {
        WindController.OnWindApply -= OnWind;
    }
}
