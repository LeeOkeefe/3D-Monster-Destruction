using UnityEngine;

namespace Objects.Destructible.Definition
{
    public abstract class DestructibleObject : MonoBehaviour
    {
        // Shared properties across all destructible objects
        //
        public float currentHealth;
        public float maxHealth;
        public float scoreAwarded;

        protected bool IsObjectDestroyed => currentHealth <= 0;
        protected void AddScore() => ScoreManager.AddScore(scoreAwarded);

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
