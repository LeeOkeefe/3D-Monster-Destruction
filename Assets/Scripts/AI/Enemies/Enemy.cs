using Player;
using Unity.Collections;
using UnityEngine;

namespace AI
{
    internal abstract class Enemy : MonoBehaviour, IAttack, IDeathHandler
    {
        [ReadOnly]
        protected float currentHealth;
        [SerializeField]
        protected float maxHealth;
        [SerializeField]
        protected float distanceToAttackTarget;
        [SerializeField]
        protected float environmentalDamage = 50f;
        [SerializeField]
        protected float timeBetweenAttacks = 5f;
        [SerializeField]
        protected float scoreAwarded;

        protected Transform PlayerTransform => GameManager.instance.player.transform;
        protected PlayerStats PlayerStats => GameManager.instance.playerStats;
        protected bool EnemyIsDead => currentHealth <= 0;

        public abstract void Attack();

        public abstract void HandleDeath();

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
            return Vector3.Distance(transform.position, PlayerTransform.position) <= distance;
        }

        /// <summary>
        /// Damages the current health of the enemy
        /// </summary>
        protected void Damage(float damage)
        {
            currentHealth -= damage;
        }
    }
}
