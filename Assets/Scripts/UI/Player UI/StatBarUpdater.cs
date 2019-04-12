using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Player_UI
{
    internal sealed class StatBarUpdater : MonoBehaviour
    {
        [SerializeField]
        private Image healthBar;
        [SerializeField]
        private Image staminaBar;
        [SerializeField]
        private PlayerStats playerStats;
        
        private void Update()
        {
            UpdateBar(healthBar, playerStats.CurrentHealth, playerStats.maxHealth);
            UpdateBar(staminaBar, playerStats.CurrentStamina, playerStats.maxStamina);
        }

        /// <summary>
        /// Updates the UI bar fill amount to the current value
        /// </summary>
        private void UpdateBar(Image bar, float currentValue, float maxValue)
        {
            bar.fillAmount = currentValue / maxValue;
        }
    }
}
