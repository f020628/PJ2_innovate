using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void OnCollisionEnter2D(Collision2D collision)
    {
        string[] obstacleTags = { "Obstacle", "Balloon" };

        if (System.Array.Exists(obstacleTags, tag => tag == collision.gameObject.tag))
        {
            SceneManager.LoadScene("Fail"); 
        }
       
        if (collision.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene("Win");  
        }
    }
}
