using Platformer.Core;
using Platformer.Mechanics;
using static Platformer.Core.Simulation;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player's health reaches 0. This usually results in a 
    /// PlayerDeath event.
    /// </summary>
    public class HealthIsZero : Simulation.Event<HealthIsZero>
    {
        public Health health;

        public override void Execute()
        {
            if (health != null)
            {
                // Ensure the health is set to zero and update the health bar
                health.Die();
                // Schedule the PlayerDeath event
                Schedule<PlayerDeath>();
            }
        }
    }
}