using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PaperPlane : MonoBehaviour
{
    [Header("Particle System")]
    public new ParticleSystem particleSystem;
    [Space(15)]
    public float windMultiply = 1;
    public float maxHorizontalV = 5;
    public float maxVerticalV = 5;
    public float naturalRotateDecay = 20;
    public float windSpinSpeed = 5;
    [Range(0.01f, 1)]
    public float verticalDampRatio;
    [Range(0.01f, 1)]
    public float horizontalDampRatio;
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
        verticalV = 0;
        rb.velocity = new Vector2(horizontalV, verticalV);
    }

    void Update()
    {
        angleOffset = Vector2.Dot(transform.right, Vector2.down) + 0.02f;
        windEfficiency = Mathf.Lerp(0.1f, 1, Mathf.Abs(Vector2.Dot(transform.up, wind.normalized)));

        horizontalV = rb.velocity.x;
        verticalV = rb.velocity.y;

        //rotation
        ChangeRotation();

        //velocity
        horizontalV = ChangeVelocityX();
        verticalV = ChangeVelocityY();

        horizontalV = Mathf.Clamp(horizontalV, 0, maxHorizontalV);
        verticalV = Mathf.Max(Mathf.Min(verticalV, maxVerticalV), -maxVerticalV * 1.4f);
        rb.velocity = new Vector2(horizontalV, verticalV);
       // Debug.Log(rb.velocity);

    }

    private float ChangeVelocityX()
    {
        float rotationOffset = Mathf.Lerp(0.2f, 1, Mathf.Abs(Vector2.Dot(transform.right, Vector2.right) + 0.02f)); 
        float decay = horizontalDampRatio * maxHorizontalV * Time.deltaTime * rotationOffset;
        float conversion = Mathf.Max(Vector2.Dot(new Vector2(1, 1).normalized, transform.right), Vector2.Dot(new Vector2(1, -1).normalized, transform.right));
        //Debug.Log(conversion);
        conversion = Mathf.Clamp((conversion - Mathf.Sqrt(2) / 2 + float.Epsilon) / (1 - Mathf.Sqrt(2)/2) , 0, 1);
        
        conversion *= Mathf.Sign(angleOffset) * convertionRatio * Time.deltaTime;
        float value = horizontalV - decay + wind.x * (windEfficiency + 0.15f) + conversion - XnaturalDecay * Time.deltaTime;// - Mathf.Abs(angleOffset) * verticalDecayRatio * Time.deltaTime;
        return value;
    }

    private float ChangeVelocityY()
    {
        //if good & fast, hold speed
        float speedOffset = Mathf.Clamp((maxHorizontalV - rb.velocity.x) / maxHorizontalV, 0.1f, 1);
        speedOffset = Mathf.Lerp(0.2f, 1.2f, speedOffset);
        float rotationOffset = Mathf.Lerp(0.2f, 1, Mathf.Abs(angleOffset));
        float decay = verticalDampRatio * maxVerticalV * Time.deltaTime * rotationOffset * speedOffset;

        //float xConversion = Mathf.Cos(transform.rotation.eulerAngles.z * Mathf.Deg2Rad) * Mathf.Sign(angleOffset) * horizontalV;
        float value = verticalV - decay + wind.y * windEfficiency - rotationOffset * YnaturalDecay * Time.deltaTime;// - (0.1f + angleOffset) * accelerationRatio * Time.deltaTime;
        return value;
    }

    private void ChangeRotation()
    {
        float maxV = maxHorizontalV;
        float speedOffset = Mathf.Clamp( (maxV - rb.velocity.x)/ maxV, 0.1f, 1);
        speedOffset = Mathf.Lerp(0.2f, 1.5f, speedOffset);
        float localAngleOffset = Mathf.Lerp(0.2f, 1.5f, Mathf.Abs(angleOffset));
        float rotationResult = transform.rotation.eulerAngles.z;
        //wind
        if (wind.magnitude != 0)
        {
            float target = Vector3.SignedAngle(transform.right, wind.normalized, Vector3.forward);
            rotationResult += target * Time.deltaTime * windEfficiency * windSpinSpeed;
        }
        else
        {
            float target = Vector3.SignedAngle(transform.right, rb.velocity, Vector3.forward);
            rotationResult += target * Time.deltaTime * localAngleOffset * speedOffset - naturalRotateDecay * Time.deltaTime;

        }

        if (rotationResult >= 90 && rotationResult <= 180)
        {
            rotationResult = 90;
        }
        else if (rotationResult <= 270 && rotationResult >= 180)
        {
            rotationResult = 270;
        }

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, rotationResult));
    }

    public void ReceiveWind(Vector2 blow)
    {
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[particleSystem.particleCount];
;
        wind = blow * windMultiply;
        particleSystem.GetParticles(particles);
        for(int i = 0; i < particles.Length; i++)
        {
            particles[i].velocity = wind.normalized;
        }
    }

   
}
