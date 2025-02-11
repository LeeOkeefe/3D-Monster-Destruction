﻿using System.Collections;
using UnityEngine;

namespace Player
{
    internal sealed class PlayerStats : MonoBehaviour
    {
        public float CurrentHealth { get; private set; }
        public float CurrentStamina { get; private set; }
        public float BoostedDamage { get; private set; }
        public float BoostedDefence { get; private set; }
        public bool IsDead => CurrentHealth <= 0;
        public bool CanRun { get; private set; }
        public float TotalDamage => baseDamage + BoostedDamage;
        private Animator Anim => GameManager.Instance.playerAnim;

        [SerializeField]
        public float maxHealth;
        [SerializeField]
        public float maxStamina;
        [SerializeField]
        public float baseDamage = 10f;

        [SerializeField]
        private Renderer m_Renderer;

        private Color m_OriginalColor;

        private static readonly int Dead = Animator.StringToHash("Dead");

        private void Start()
        {
            m_OriginalColor = m_Renderer.material.color;

            CurrentHealth = maxHealth;
            CurrentStamina = maxStamina;

            CanRun = true;
        }

        /// <summary>
        /// Switches material colour to produce a flash effect
        /// </summary>
        private IEnumerator DamageFeedback()
        {
            if (BoostedDefence > 0)
            {
                m_Renderer.material.SetColor("_Color", Color.blue);
                yield return new WaitForSeconds(0.1F);
                m_Renderer.material.SetColor("_Color", m_OriginalColor);
            }
            else
            {
                m_Renderer.material.SetColor("_Color", Color.red);
                yield return new WaitForSeconds(0.1F);
                m_Renderer.material.SetColor("_Color", m_OriginalColor);
            }
        }

        /// <summary>
        /// Adds X amount of health to the current health
        /// </summary>
        public void AddHealth(float health)
        {
            CurrentHealth += health;
            ClampStats();

            if (CurrentHealth <= 0)
            {
                Anim.SetTrigger(Dead);
                GameManager.Instance.Invoke(nameof(GameManager.GameOver), 1.75F);
            }
        }

        /// <summary>
        /// Check for boosted defence, then apply damage
        /// </summary>
        public void Damage(float damage)
        {
            StartCoroutine(nameof(DamageFeedback));

            if (BoostedDefence > 0)
            {
                var total = damage / 100 * BoostedDefence;
                AddHealth(-total);
            }
            else
            {
                AddHealth(-damage);
            }
        }

        /// <summary>
        /// Adds X amount of stamina to the current stamina
        /// </summary>
        public void AddStamina(float stamina)
        {
            CurrentStamina += stamina;

            ClampStats();
        }

        /// <summary>
        /// Enforce boosted damage being set through a method,
        /// so we can ensure it is always set to the correct amount
        /// </summary>
        public void BoostDamage(float amount)
        {
            BoostedDamage = amount;
        }

        /// <summary>
        /// Resets the boosted damage back to 0
        /// </summary>
        public void ResetBoostedDamage()
        {
            BoostedDamage = 0f;
        }

        /// <summary>
        /// Boosts the defence of the player, the amount of defence
        /// acts as a percentage of damage that will be blocked
        /// </summary>
        public void BoostDefence(float amount)
        {
            BoostedDefence += amount;
        }

        /// <summary>
        /// Resets the boosted defence back to 0
        /// </summary>
        public void ResetBoostedDefence()
        {
            BoostedDefence = 0f;
        }

        /// <summary>
        /// Regenerates stamina over time if current value is
        /// below the maximum <see cref="RegenerateOverTime"/>
        /// </summary>
        public void RegenerateStamina()
        {
            if (GameManager.Instance.IsGamePaused)
                return;

            if (CurrentStamina < maxStamina)
            {
                StartCoroutine(nameof(RegenerateOverTime));
            }
        }

        /// <summary>
        /// Depletes stamina over time if current value is above 0
        /// </summary>
        public void DepleteStamina()
        {
            if (!GameManager.Instance.player.PlayerIsMoving)
                return;

            if (CurrentStamina > 0)
            {
                AddStamina(-0.3f);
            }

            if (CurrentStamina <= 0)
            {
                CanRun = false;
            }
        }

        private IEnumerator RegenerateOverTime()
        {
            yield return new WaitForSeconds(1f);
            AddStamina(0.15f);

            if (CurrentStamina >= 10)
            {
                CanRun = true;
            }
        }

        /// <summary>
        /// Ensures the stat does not go above the maximum, or below 0
        /// </summary>
        private void ClampStats()
        {
            if (CurrentHealth > maxHealth)
                CurrentHealth = maxHealth;

            if (CurrentStamina > maxStamina)
                CurrentStamina = maxStamina;

            if (CurrentHealth < 0)
                CurrentHealth = 0;

            if (CurrentStamina < 0)
                CurrentStamina = 0;
        }
    }
}
