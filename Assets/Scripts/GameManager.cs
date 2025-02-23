using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Reference to your VictoryScreen Panel
    public GameObject victoryScreen;

    private int totalEnemies;

    private void Awake()
    {
        // Use the new recommended method to find knights
        Knight[] knights = FindObjectsByType<Knight>(FindObjectsSortMode.None);
        totalEnemies = knights.Length;
    }

    public void EnemyDefeated()
    {
        totalEnemies--;
        if (totalEnemies <= 0)
        {
            ShowVictoryScreen();
        }
    }

    private void ShowVictoryScreen()
    {
        // (Optional) Pause the game
        Time.timeScale = 0f;

        // Enable the VictoryScreen Panel
        if (victoryScreen != null)
        {
            victoryScreen.SetActive(true);
        }
    }
}



