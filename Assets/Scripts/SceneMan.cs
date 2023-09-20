using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMan : MonoBehaviour
{
     public void LoadGameScene()
    {
        SceneManager.LoadScene("Level_1_Building"); 
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameOver()
    {
        SceneManager.LoadScene("Fail");  
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
             SceneManager.LoadScene("Level_1_Building"); 
    }
}
