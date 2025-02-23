using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;           // Fixed spelling: healthSlider instead of heatlhSlider
    public TMP_Text healthBarText;

    Damageable playerDamageable;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.LogError("No player found in the scene. Make sure it has tag 'Player'");
            return; // Exit if no player found.
        }

        // Get the Damageable component from the player GameObject
        playerDamageable = player.GetComponent<Damageable>();
        if(playerDamageable == null)
        {
            Debug.LogError("No Damageable component found on the player.");
        }
    }

    // Start is called once before the first frame update
    void Start()
    {
        if(playerDamageable != null)
        {
            healthSlider.value = CalculateSliderPercentage(playerDamageable.Health, playerDamageable.MaxHealth);
            healthBarText.text = "HP " + playerDamageable.Health + " / " + playerDamageable.MaxHealth;
        }
    }

    private void OnEnable()
    {
        if (playerDamageable != null)
            playerDamageable.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    private void OnDisable()
    {
        if (playerDamageable != null)
            playerDamageable.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    private float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }

    // Called when the player's health changes
    private void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        // Corrected: use maxHealth instead of newHealth for the denominator
        healthSlider.value = CalculateSliderPercentage(newHealth, maxHealth);
        healthBarText.text = "HP " + newHealth + " / " + maxHealth;
    }
}
