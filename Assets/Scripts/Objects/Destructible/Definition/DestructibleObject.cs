using Objectives;
using Objects.Destructible.Objects;
using UnityEngine;

namespace Objects.Destructible.Definition
{
    internal abstract class DestructibleObject : MonoBehaviour, IDestructible
    {
        // Shared properties across all destructible objects
        //
        public float currentHealth;
        public float maxHealth;
        public float scoreAwarded;

        protected bool IsObjectDestroyed => currentHealth <= 0;
        protected void AddScore() => ScoreManager.AddScore(scoreAwarded);

        public virtual void Damage(float damage)
        {
            currentHealth -= damage;
        }

        public abstract void Destruct();

        /// <summary>
        /// If object health is less than or equal to 0,
        /// destroy the gameObject
        /// </summary>
        protected void DestroyObject()
        {
            if (!IsObjectDestroyed)
                return;

            Destroy(gameObject);
        }
    }
}
