using UnityEngine;
using System.Collections; // For IEnumerator/Coroutines
using UnityEngine.UI;
using TMPro; // For TextMeshPro support
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Victory Screen UI
    public GameObject victoryScreen;
    public GameObject defeatScreen;

    private int totalEnemies;
    private bool isGameOver = false;

    private void Awake()
    {
        // Use new recommended methods to find enemies
        Knight[] knights = FindObjectsByType<Knight>(FindObjectsSortMode.None);
        FlyingEye[] flyingEyes = FindObjectsByType<FlyingEye>(FindObjectsSortMode.None);
        totalEnemies = knights.Length + flyingEyes.Length;
    }

    public void EnemyDefeated()
    {
        totalEnemies--;
        if (totalEnemies <= 0)
        {
            // When the last enemy is defeated, start the win sequence
            StartCoroutine(WinSequence());
        }
    }

    private IEnumerator WinSequence()
    {
        // 1) Tell the player to do a "win pose" animation.
        PlayerController player = FindFirstObjectByType<PlayerController>();
        if (player != null)
        {
            Animator playerAnimator = player.GetComponent<Animator>();
            if (playerAnimator != null)
            {
                playerAnimator.SetTrigger("winPose"); // Trigger win animation
            }
        }

        // 2) Wait for a short delay (e.g., 2 seconds) so the win pose can be seen.
        yield return new WaitForSeconds(2f);

        // 3) Show the victory screen (and pause the game).
        ShowVictoryScreen();
    }

    private void ShowVictoryScreen()
    {
        Time.timeScale = 0f; // Pause the game
        if (victoryScreen != null)
        {
            victoryScreen.SetActive(true);
        }
    }

    // ============================ GAME OVER LOGIC ============================ //

    public void TriggerGameOver()
    {
        if (!isGameOver)
        {
            Debug.Log("GameManager.TriggerGameOver() called!");
            isGameOver = true;
            StartCoroutine(GameOverSequence());
        }
    }

    private IEnumerator GameOverSequence()
    {
        Debug.Log("GameOverSequence started. Waiting 2s...");
        // 1) Wait 2 seconds so the death animation finishes in real-time
        yield return new WaitForSeconds(2f);

        Debug.Log("Enabling DefeatScreen!");
        // 2) Show the Defeat Screen
        if (defeatScreen != null)
        {
            defeatScreen.SetActive(true);  // Enable the DefeatScreen panel
        }

        // 3) Pause the game so the player can't move
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (isGameOver && Input.GetKeyDown(KeyCode.Return))
        {
            Time.timeScale = 1f; // Unpause the game
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the level
        }
    }   

    
}






