using System.Diagnostics;
using UnityEngine;

namespace Abilities.Definitions
{
    internal sealed class Flamethrower : Ability
    {
        private PlayerAbility m_PlayerAbility;

        [SerializeField]
        private float abilityDuration = 5f;
        [SerializeField]
        private float damageMultiplier = 2f;
        [SerializeField]
        private Transform particlePosition;

        private ParticleSystem[] m_ParticleSystems;

        private Stopwatch m_FlamethrowerAbilityTimer;

        private bool m_HasHappenedOnce;

        private void Start()
        {
            m_PlayerAbility = new PlayerAbility();
            m_FlamethrowerAbilityTimer = new Stopwatch();
            m_ParticleSystems = GameObject.FindGameObjectWithTag("Flamethrower").GetComponentsInChildren<ParticleSystem>();
        }

        private void Update()
        {
            if (IsAbilityActive)
            {
                FlamethrowerAttack(damageMultiplier);

                foreach (var particle in m_ParticleSystems)
                {
                    particle.transform.position = particlePosition.position;
                }
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

            foreach (var particle in m_ParticleSystems)
            {
                particle.Play();
            }

            m_HasHappenedOnce = false;
            abilityButton.enabled = false;
            ActiveAbilities.ActivateAbility(this);
            m_FlamethrowerAbilityTimer.Start();
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
        /// then resets it back to 0
        /// </summary>
        private void FlamethrowerAttack(float multiplier)
        {
            if (!m_HasHappenedOnce)
            {
                m_HasHappenedOnce = true;
                m_PlayerAbility.StrengthBoost(multiplier);
                ActiveAbilities.UpdateSlot(abilityButton.image, IsAbilityActive);
            }

            if (m_FlamethrowerAbilityTimer.Elapsed.Seconds >= abilityDuration)
            {
                foreach (var particle in m_ParticleSystems)
                {
                    particle.Stop();
                }

                ActiveAbilities.DeactivateAbility(this);
                m_FlamethrowerAbilityTimer.Reset();
                m_PlayerAbility.ResetStrengthBoost();
                ActiveAbilities.UpdateSlot(abilityButton.image, IsAbilityActive);
            }
        }
    }
}
