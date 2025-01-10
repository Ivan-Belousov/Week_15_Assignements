using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when a Player collides with an Enemy.
    /// </summary>
    /// <typeparam name="PlayerEnemyCollision"></typeparam>
    public class PlayerEnemyCollision : Simulation.Event<PlayerEnemyCollision>
    {
        public EnemyController enemy;
        public PlayerController player;

        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            // Determine if the player is above the enemy
            var willHurtEnemy = player.Bounds.center.y >= enemy.Bounds.max.y;

            if (willHurtEnemy)
            {
                // Handle damage to the enemy
                var enemyHealth = enemy.GetComponent<Health>();
                if (enemyHealth != null)
                {
                    enemyHealth.Decrement(); // Decrease enemy health
                    if (!enemyHealth.IsAlive) // If the enemy dies
                    {
                        Schedule<EnemyDeath>().enemy = enemy; // Schedule EnemyDeath event
                        player.Bounce(2); // Bounce the player slightly
                    }
                    else
                    {
                        player.Bounce(7); // Bounce the player more for a non-lethal hit
                    }
                }
                else
                {
                    // If the enemy has no health component, treat it as instantly dead
                    Schedule<EnemyDeath>().enemy = enemy;
                    player.Bounce(2);
                }
            }
            else
            {
                // Handle damage to the player
                var playerHealth = player.GetComponent<Health>();
                if (playerHealth != null && playerHealth.IsAlive)
                {
                    playerHealth.Decrement(); // Decrease player health
                }

                // If player health reaches 0, trigger PlayerDeath
                if (playerHealth != null && !playerHealth.IsAlive)
                {
                    Schedule<PlayerDeath>();
                }
            }
        }
    }
}


