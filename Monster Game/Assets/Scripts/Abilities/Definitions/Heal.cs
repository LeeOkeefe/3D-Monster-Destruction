using System.Diagnostics;
using UnityEngine;

namespace Abilities.Definitions
{
    internal sealed class Heal : Ability
    {
        private PlayerAbility m_PlayerAbility;

        [SerializeField]
        private float healAmount = 10f;

        private void Start()
        {
            m_PlayerAbility = new PlayerAbility();
        }

        private void Update()
        {
            // TODO : REMOVE THIS AFTER TESTING IS COMPLETE!
            if (Input.GetKeyDown(KeyCode.H))
            {
                GameManager.instance.playerStats.Damage(10);
                ScoreManager.AddScore(15000);
            }
        }

        // Ensure we can't use the button whilst the ability is active,
        // heals the player
        //
        public override void ActivateAbility()
        {
            if (m_CooldownActive)
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
