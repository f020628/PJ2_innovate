using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperPlane : MonoBehaviour
{
    public float maxHorizontalV = 5;
    public float maxVerticalV = 5;
    public float naturalRotateDecay = 20;
    public float drag = 5;
    [Range(0,1.5f)]
    public float verticalDecayRatio;
    [Range(0, 1.5f)]
    public float horizontalDecayRatio;
    public float XnaturalDecay;
    public float YnaturalDecay;
    public float convertionRatio;
    /// <summary>
    /// down is 1, up is -1
    /// </summary>
    private float angleOffset = 0;
    /// <summary>
    /// perpendicular to wind? 0.2 to 1
    /// </summary>
    private float windEfficiency = 0;
    Rigidbody2D rb;
    float horizontalV;
    float verticalV;
    Vector2 wind = Vector2.zero;
    void Awake()
    {   
        rb = GetComponent<Rigidbody2D>();

        horizontalV = 5;
        verticalV = 5;
        rb.velocity = new Vector2(horizontalV, verticalV);
        transform.rotation = Quaternion.Euler(Vector2.zero);
    }

    void Update()
    {
        angleOffset = Vector2.Dot(transform.right, Vector2.down) + 0.02f;
        windEfficiency = Mathf.Lerp(0.1f, 1, Mathf.Abs(Vector2.Dot(transform.up, wind.normalized)));

        horizontalV = rb.velocity.x;
        verticalV = rb.velocity.y;

        //rotation
        transform.rotation = Quaternion.Euler(ChangeRotation());
        

        //velocity
        //horizontalV = ChangeVelocityX();
        verticalV = ChangeVelocityY();

        horizontalV = Mathf.Clamp(horizontalV, 0, maxHorizontalV);
        verticalV = Mathf.Max(Mathf.Min(verticalV, maxVerticalV), -maxVerticalV);
        rb.velocity = new Vector2(horizontalV, verticalV);
       // Debug.Log(rb.velocity);
    }

    private float ChangeVelocityX()
    {
        //resistance 
        float weight = Mathf.Abs(Vector3.Dot(rb.velocity.normalized, transform.up));
        float offset = 1 - weight * horizontalDecayRatio * Time.deltaTime ;
        Debug.Log(offset);
        float value = horizontalV * offset + wind.x * windEfficiency - XnaturalDecay * Time.deltaTime;// - Mathf.Abs(angleOffset) * verticalDecayRatio * Time.deltaTime;
        float weight2 = Mathf.Cos(transform.rotation.eulerAngles.z);//Mathf.Abs(Vector2.Dot(transform.right, Vector2.right) - Mathf.Cos(Mathf.Deg2Rad * 45)) + 0.01f;
        value += weight2 * convertionRatio * Mathf.Sign(angleOffset);
        Debug.Log(value);
        return value;
    }

    private float ChangeVelocityY()
    {
        //resistance slows down
        float weight = Mathf.Abs(Vector3.Dot(rb.velocity.normalized, transform.right));
        float offset = 1 - weight * verticalDecayRatio * Time.deltaTime;

        float value = verticalV * offset - wind.y * windEfficiency - YnaturalDecay * Time.deltaTime ;// - (0.1f + angleOffset) * accelerationRatio * Time.deltaTime;
        float weight2 = Mathf.Cos(transform.rotation.eulerAngles.z);
        value += weight2 * convertionRatio * Mathf.Sign(angleOffset);
        Debug.Log(weight2);
        return value;
    }

    private Vector3 ChangeRotation()
    {
        float amount = Time.deltaTime * naturalRotateDecay;
        float speedOffset = Mathf.Clamp(new Vector2(maxHorizontalV, maxVerticalV).magnitude / rb.velocity.magnitude, 0.05f, 1);
        speedOffset = Mathf.Lerp(0.5f, 1.5f, speedOffset);
        float localAngleOffset = Mathf.Sign(angleOffset) * Mathf.Lerp(0.1f, 1, Mathf.Abs(angleOffset));
        float rotationResult = transform.rotation.eulerAngles.z - amount * localAngleOffset * speedOffset;
        //wind
        float target = Vector3.SignedAngle(transform.right, wind.normalized, Vector3.forward);
        rotationResult += target * windEfficiency;
        //Debug.Log(transform.right, wind.normalized, Vector3.forward);

        if (rotationResult >= 90 && rotationResult <= 180)
        {
            rotationResult = 90;
        }
        else if (rotationResult <= 270 && rotationResult >= 180)
        {
            rotationResult = 270;
        }

        return new Vector3(0, 0, rotationResult);
    }

    public void ReceiveWind(Vector2 blow)
    {
        wind = blow;
    }
}
