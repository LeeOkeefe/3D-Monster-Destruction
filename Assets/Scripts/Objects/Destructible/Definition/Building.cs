using System;
using Player;

namespace Objects.Destructible.Definition
{
    internal sealed class Building
    {
        private PlayerStats PlayerStats => GameManager.instance.playerStats;

        /// <summary>
        /// Damage the destructibleObject and add the score per hit
        /// </summary>
        public void Damage(DestructibleObject destructibleObject, float damage)
        {
            var scorePerHit = destructibleObject.scoreAwarded / destructibleObject.maxHealth;
            destructibleObject.currentHealth -= damage;

            ScoreManager.AddScore(damage * scorePerHit, 10);
        }

        /// <summary>
        /// Checks the total damage and applies it to the building
        /// </summary>
        public void FlamethrowerDamage(DestructibleObject destructibleObject, Action action)
        {
            if (PlayerStats.TotalDamage <= 10)
                return;

            Damage(destructibleObject, PlayerStats.TotalDamage);

            if (destructibleObject.currentHealth <= 0)
                action();
        }
    }
}
