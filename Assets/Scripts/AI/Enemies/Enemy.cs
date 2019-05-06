using Player;
using Unity.Collections;
using UnityEngine;

namespace AI.Enemies
{
    internal abstract class Enemy : MonoBehaviour, IAttack, IDeathHandler, ISubmerged
    {
        [ReadOnly]
        protected float CurrentHealth;
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

        protected Transform PlayerTransform => GameManager.Instance.player.transform;
        protected bool EnemyIsDead => CurrentHealth <= 0;

        public abstract void Attack();

        public abstract void HandleDeath();

        /// <summary>
        /// Handle death if thrown into water
        /// </summary>
        public void Underwater()
        {
            gameObject.GetComponent<Collider>().isTrigger = true;
            Damage(CurrentHealth);

            if (!EnemyIsDead)
            {
                Debug.Log("Enemy is not dead");
            }

            Destroy(gameObject);
            ScoreManager.AddScore(scoreAwarded);
        }

        /// <summary>
        /// Set the current health to be the max, so we can avoid exposing the currentHealth
        /// </summary>
        protected void InitializeHealth()
        {
            CurrentHealth = maxHealth;
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
            CurrentHealth -= damage;
        }
    }
}
