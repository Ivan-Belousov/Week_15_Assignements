using Platformer.Core;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the player has died.
    /// </summary>
    public class PlayerDeath : Simulation.Event<PlayerDeath>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            var player = model.player;

            // Proceed only if the player is not alive
            if (!player.health.IsAlive)
            {
                // Ensure the player's health is zero and update the health bar
                player.health.Die();

                // Disable player control and camera follow
                model.virtualCamera.Follow = null;
                model.virtualCamera.LookAt = null;
                player.controlEnabled = false;

                // Play death audio if available
                if (player.audioSource && player.ouchAudio)
                    player.audioSource.PlayOneShot(player.ouchAudio);

                // Trigger death animation
                player.animator.SetTrigger("hurt");
                player.animator.SetBool("dead", true);

                // Schedule player respawn after 2 seconds
                var respawn = Simulation.Schedule<PlayerSpawn>(2);
                respawn.player = player;
            }
        }
    }
}
