using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private float cameraFollowT = 1.0f;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject levelBounds;
    private TilemapCollider2D boundsMap;

    private float minYPosition = 0;

    private void Start()
    {
        boundsMap = levelBounds.GetComponent<TilemapCollider2D>();
        minYPosition = boundsMap.bounds.center.y;
    }

    // Update is called once per frame
    void Update()
    {
        // Lerp camera towards player position
        transform.position = Vector2.Lerp(transform.position, player.transform.position + Vector3.right * 5, cameraFollowT * Time.deltaTime);
        float newY = Mathf.Clamp(transform.position.y, boundsMap.bounds.center.y - minYPosition, boundsMap.bounds.center.y + minYPosition);
        transform.position = new Vector2(transform.position.x, newY);
        
        // Clamp camera position to keep inside the level
        transform.SetPositionAndRotation(Vector3.Lerp(transform.position, player.transform.position + Vector3.right * 5, cameraFollowT * Time.deltaTime) + Vector3.forward * -10, transform.rotation);
    }
}
