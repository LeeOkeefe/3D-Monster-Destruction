using UnityEngine;

namespace Abilities.Definitions
{
    internal sealed class Heal : Ability
    {
        private PlayerAbility m_PlayerAbility;

        [SerializeField]
        private float healAmount = 20f;

        private void Start()
        {
            m_PlayerAbility = new PlayerAbility();
        }

        // Ensure we can't use the button whilst the ability is active,
        // heals the player
        //
        public override void ActivateAbility()
        {
            if (m_CooldownActive)
                return;

            if (!ScoreManager.HasScore(abilityCostInPoints))
                return;

            HandleCost();
            OnStart();

            abilityButton.enabled = false;
            ActiveAbilities.ActivateAbility(this);
            m_PlayerAbility.Heal(healAmount);
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
            ActiveAbilities.DeactivateAbility(this);
        }
    }
}
