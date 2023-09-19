using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Balloon":
                Balloon balloon;
                if (collision.gameObject.TryGetComponent<Balloon>(out balloon))
                    balloon.Pop();
                else
                    Debug.LogWarning("Object " + collision.gameObject.name + " is tagged as a balloon, but there's no balloon component on it.");
                break;
            case "Bird":
                Debug.Log("Game Over!");
                break;

            default:
                break;
        }
    }
}
