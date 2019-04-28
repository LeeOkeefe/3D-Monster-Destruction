using AI;
using Player;
using UnityEngine;

namespace Environment
{
    internal sealed class OceanDamage : MonoBehaviour
    {
        private PlayerStats PlayerStats => GameManager.instance.playerStats;

        // If the player collides, call DamagePlayer every 0.25 seconds
        //
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                InvokeRepeating(nameof(DamagePlayer), 0.1f, 0.25f);
            }

            var handler = other.gameObject.GetComponent<ISubmerged>();
            handler?.Underwater();
        }

        // Stop calling DamagePlayer if we stop colliding
        //
        private void OnCollisionExit(Collision other)
        {
            CancelInvoke(nameof(DamagePlayer));
        }

        // Damage the player 5 HP
        //
        private void DamagePlayer()
        {
            PlayerStats.Damage(5);
        }
    }
}
