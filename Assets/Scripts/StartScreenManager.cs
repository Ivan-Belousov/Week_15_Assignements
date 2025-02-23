using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreenManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // Load your GameplayScene 
            // Make sure the scene name matches exactly what’s in File→Build Settings
            SceneManager.LoadScene("GameplayScene");
        }

        // ESC to quit (optional, if you want to allow quitting from start screen)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();

        }
    }
}