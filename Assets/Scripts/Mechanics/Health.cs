using System;
using Platformer.Gameplay;
using UnityEngine;
using UnityEngine.UI;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    public class Health : MonoBehaviour
    {
        public int maxHP = 2; // Default maximum health points
        public Slider healthBar; // Reference to the health bar slider

        public bool IsAlive => currentHP > 0;

        public int currentHP { get; private set; } // Public getter, private setter

        public void Increment()
        {
            currentHP = Mathf.Clamp(currentHP + 1, 0, maxHP);
            UpdateHealthBar(); // Update the health bar slider
        }

        public void Decrement()
        {
            currentHP = Mathf.Clamp(currentHP - 1, 0, maxHP);
            UpdateHealthBar(); // Update the health bar slider

            if (currentHP == 0)
            {
                // Trigger death logic only when health reaches 0
                Schedule<HealthIsZero>();
            }
        }

        public void Die()
        {
            currentHP = 0;
            UpdateHealthBar();
        }

        public void ResetHealth() // Correctly defined method
        {
            currentHP = maxHP; // Reset health to maximum
            UpdateHealthBar(); // Update the health bar slider
        }

        void Awake()
        {
            currentHP = maxHP; // Initialize health
            if (healthBar != null)
            {
                healthBar.maxValue = maxHP; // Set max value of slider
                healthBar.value = currentHP; // Initialize slider value
            }
        }

        public void UpdateHealthBar()
        {
            if (healthBar != null)
            {
                healthBar.value = currentHP; // Sync slider with current HP
            }
        }
    }
}

