using Platformer.Core;
using Platformer.Mechanics;
using UnityEngine;

namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when a player collides with a token.
    /// </summary>
    public class PlayerTokenCollision : Simulation.Event<PlayerTokenCollision>
    {
        public PlayerController player;
        public TokenInstance token;

        public override void Execute()
        {
            // Play the token collection sound
            AudioSource.PlayClipAtPoint(token.tokenCollectAudio, token.transform.position);

            // Increment the token count on the player
            player.AddToken();
        }
    }
}


