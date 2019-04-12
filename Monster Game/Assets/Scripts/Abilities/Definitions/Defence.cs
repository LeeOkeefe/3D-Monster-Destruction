using System.Diagnostics;
using UnityEngine;

namespace Abilities.Definitions
{
    internal sealed class Defence : Ability
    {
        private PlayerAbility m_PlayerAbility;

        [SerializeField]
        private float abilityDuration = 7.5f;
        [SerializeField]
        private float defencePercentage = 50f;

        private Stopwatch m_DefenceAbilityTimer;

        private bool m_HasHappenedOnce;

        private void Start()
        {
            m_PlayerAbility = new PlayerAbility();
            m_DefenceAbilityTimer = new Stopwatch();
        }

        private void Update()
        {
            if (IsAbilityActive)
            {
                DefenceBoost();
            }
        }

        // Ensure we can't use the button whilst the ability
        // is currently is use
        //
        public override void ActivateAbility()
        {
            if (m_CooldownActive)
                return;

            HandleCost();
            OnStart();

            m_HasHappenedOnce = false;
            abilityButton.enabled = false;
            ActiveAbilities.ActivateAbility(this);
            m_DefenceAbilityTimer.Start();
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
        /// Sets the player's boosted defence for the abilities duration,
        /// then resets it back to 0
        /// </summary>
        private void DefenceBoost()
        {
            if (!m_HasHappenedOnce)
            {
                m_PlayerAbility.DefenceBoost(defencePercentage);
                m_HasHappenedOnce = true;
                ActiveAbilities.UpdateSlot(abilityButton.image, IsAbilityActive);
            }

            if (m_DefenceAbilityTimer.Elapsed.Seconds >= abilityDuration)
            {
                m_PlayerAbility.ResetDefenceBoost();
                ActiveAbilities.DeactivateAbility(this);
                m_DefenceAbilityTimer.Reset();
                ActiveAbilities.UpdateSlot(abilityButton.image, IsAbilityActive);
            }
        }
    }
}
