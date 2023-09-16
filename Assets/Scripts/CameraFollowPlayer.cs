using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private float cameraFollowT = 1.0f;
    [SerializeField] private GameObject player;

    // Update is called once per frame
    void Update()
    {
        // Lerp camera towards player position
        transform.position = Vector2.Lerp(transform.position, player.transform.position, cameraFollowT * Time.deltaTime);
        transform.SetPositionAndRotation(Vector3.Lerp(transform.position, player.transform.position, cameraFollowT * Time.deltaTime) + Vector3.forward * -10, transform.rotation);
    }
}
