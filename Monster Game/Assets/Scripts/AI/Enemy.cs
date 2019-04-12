using Player;
using Unity.Collections;
using UnityEngine;

namespace AI
{
    internal abstract class Enemy : MonoBehaviour
    {
        [ReadOnly]
        protected float currentHealth;
        [SerializeField]
        protected float maxHealth;
        [SerializeField]
        protected float distanceToAttackTarget;
        [SerializeField]
        protected float dealDamage = 5f;
        [SerializeField]
        protected float timeBetweenAttacks = 5f;
        [SerializeField]
        protected float scoreAwarded;

        protected Transform PlayerTransform => GameManager.instance.player.transform;
        protected PlayerStats PlayerStats => GameManager.instance.playerStats;

        /// <summary>
        /// Set the current health to be the max, so we can avoid exposing the currentHealth
        /// </summary>
        protected void InitializeHealth()
        {
            currentHealth = maxHealth;
        }

        /// <summary>
        /// Checks that our player is in within a specified range from us
        /// </summary>
        protected bool IsPlayerInRange(float distance)
        {
            return Vector3.Distance(transform.position, PlayerTransform.position) < distance;
        }
    }
}
