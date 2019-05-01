using System.Diagnostics;
using UnityEngine;

namespace Abilities.Definitions
{
    internal sealed class Strength : Ability
    {
        private PlayerAbility m_PlayerAbility;

        [SerializeField]
        private float abilityDuration = 7.5f;
        [SerializeField]
        private float damageMultiplier = 0.75f;

        private Stopwatch m_StrengthAbilityTimer;

        private bool m_HasHappenedOnce;

        private void Start()
        {
            m_PlayerAbility = new PlayerAbility();
            m_StrengthAbilityTimer = new Stopwatch();
        }

        private void Update()
        {
            if (IsAbilityActive)
            {
                StrengthBoost(damageMultiplier);
            }
        }

        // Ensure we can't use the button whilst the ability
        // is currently is use
        //
        public override void ActivateAbility()
        {
            if (m_CooldownActive)
                return;

            if (!ScoreManager.HasScore(abilityCostInPoints))
                return;

            HandleCost();
            OnStart();

            m_HasHappenedOnce = false;
            abilityButton.enabled = false;
            ActiveAbilities.ActivateAbility(this);
            m_StrengthAbilityTimer.Start();
        }

        public override void OnStart()
        {
            m_CooldownActive = true;
            timerWheel.Initialize(cooldownDuration, OnCooldownEnd);
        }

        public override void OnCooldownEnd()
        {
            m_CooldownActive = false;
            abilityButton.enabled = true;
        }

        /// <summary>
        /// Sets the player's boosted strength for the abilities duration,
        /// then resets it back to 0 once the ability duration has finished
        /// </summary>
        private void StrengthBoost(float multiplier)
        {
            if (!m_HasHappenedOnce)
            {
                m_HasHappenedOnce = true;
                m_PlayerAbility.StrengthBoost(multiplier);
                ActiveAbilities.UpdateSlot(abilityButton.image, IsAbilityActive);
            }

            if (m_StrengthAbilityTimer.Elapsed.Seconds >= abilityDuration)
            {
                ActiveAbilities.DeactivateAbility(this);
                m_StrengthAbilityTimer.Reset();
                m_PlayerAbility.ResetStrengthBoost();
                ActiveAbilities.UpdateSlot(abilityButton.image, IsAbilityActive);
            }
        }
    }
}
