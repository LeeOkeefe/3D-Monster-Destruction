using System.Collections.Generic;
using UI;
using UI.Ability_Bar;
using UI.Player_UI;
using UnityEngine;
using UnityEngine.UI;

namespace Abilities.Definitions
{
    internal abstract class Ability : MonoBehaviour
    {
        [SerializeField]
        public int abilityCostInPoints;
        [SerializeField]
        protected Button abilityButton;
        [SerializeField]
        protected float cooldownDuration = 5f;
        [SerializeField]
        protected TimerWheel timerWheel;
        [SerializeField]
        protected AbilityDescription abilityDescription;

        protected void ErrorMessage() => abilityDescription.StartCoroutine(nameof(abilityDescription.InsufficientPoints));

        protected bool m_CooldownActive;

        protected ActiveAbilities ActiveAbilities => GameManager.Instance.activeAbilities;
        protected static Dictionary<string, KeyCode> KeyCodes => GameManager.Instance.KeyCodes;

        protected bool IsAbilityActive => GameManager.Instance.activeAbilities.IsAbilityActive(this);

        /// <summary>
        /// Ensures that the player score is equal to or greater than the required
        /// amount and subtracts it if true
        /// </summary>
        protected void HandleCost()
        {
            if (!ScoreManager.HasScore(abilityCostInPoints))
                return;

            ScoreManager.SubtractScore(abilityCostInPoints);
        }

        public abstract void ActivateAbility();
        public abstract void OnStart();
        public abstract void OnCooldownEnd();
    }
}
