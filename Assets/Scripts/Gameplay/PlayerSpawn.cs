using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// This event is fired when the player should respawn after dying.
    /// </summary>
    public class PlayerSpawn : Simulation.Event<PlayerSpawn>
    {
        public PlayerController player;

        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            // Reset player's position to the spawn point
            player.transform.position = model.spawnPoint.transform.position;

            // Reset player's health using the ResetHealth method
            player.health.ResetHealth();

            // Enable player control and camera follow
            model.virtualCamera.Follow = player.transform;
            model.virtualCamera.LookAt = player.transform;
            player.controlEnabled = true;

            // Reset animations
            player.animator.SetBool("dead", false);
        }
    }
}
