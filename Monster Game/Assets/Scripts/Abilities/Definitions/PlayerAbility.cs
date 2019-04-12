using Player;

namespace Abilities.Definitions
{
    internal sealed class PlayerAbility
    {
        private PlayerStats PlayerStats => GameManager.instance.playerStats;

        /// <summary>
        /// Heals the player health by the specified amount
        /// </summary>
        public void Heal(int amount) => PlayerStats.AddHealth(amount);

        /// <summary>
        /// Heals the player by the percentage of their max health
        /// </summary>
        public void Heal(float percentage)
        {
            var healAmount = PlayerStats.maxHealth / 100 * percentage;
            PlayerStats.AddHealth(healAmount);
        }

        /// <summary>
        /// Boosts the player base strength by the multiplied amount for
        /// X amount of seconds
        /// </summary>
        public void StrengthBoost(float multiplier)
        {
            var boostedStrength = PlayerStats.baseDamage * multiplier;
            PlayerStats.BoostDamage(boostedStrength);
        }

        /// <summary>
        /// Reset boosted strength to a value of 0
        /// </summary>
        public void ResetStrengthBoost() => PlayerStats.ResetBoostedDamage();

        /// <summary>
        /// Boosts the defence by the percentage, which will block
        /// the specified percentage of incoming damage
        /// </summary>
        public void DefenceBoost(float percentage) => PlayerStats.BoostDefence(percentage);

        /// <summary>
        /// Resets the boosted defence to a value of 0
        /// </summary>
        public void ResetDefenceBoost() => PlayerStats.ResetBoostedDefence();
    }
}
