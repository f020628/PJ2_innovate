using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WindController : MonoBehaviour
{
    [SerializeField] private InputActionAsset actions;

    private InputAction windBlowStrength;
    private InputAction windBlowDirection;
    [SerializeField] float windBlowMultiplier;

    public delegate void WindApply(Vector2 dir);
    public static event WindApply OnWindApply;

    // Start is called before the first frame update
    void Start()
    {
        windBlowStrength = actions.FindAction("windBlowStrength", true); 
        windBlowDirection = actions.FindAction("windBlowDirection", true); 
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = windBlowDirection.ReadValue<Vector2>() * windBlowStrength.ReadValue<float>() * windBlowMultiplier;
        OnWindApply?.Invoke(dir);
    }

    private void OnEnable()
    {
        actions.Enable();
    }

    private void OnDisable()
    {
        actions.Disable();
    }
}
