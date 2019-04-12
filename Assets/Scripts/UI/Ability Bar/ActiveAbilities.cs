using System;
using System.Collections.Generic;
using Abilities.Definitions;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Ability_Bar
{
    internal sealed class ActiveAbilities : MonoBehaviour
    {
        private IList<Ability> m_ActiveAbilities;

        [SerializeField]
        private Image[] slots;
        [SerializeField]
        private Text[] timers;

        private void Start()
        {
            m_ActiveAbilities = new List<Ability>();

            foreach (var slot in slots)
            {
                slot.sprite = null;
                slot.color = Color.clear;
            }

            foreach (var time in timers)
            {
                time.text = string.Empty;
            }
        }

        /// <summary>
        /// Updates the slot with the ability icon if the ability is active,
        /// otherwise we set the slot to null
        /// </summary>
        public void UpdateSlot(Image abilityIcon, bool isActive)
        {
            if (isActive)
            {
                var slotIndex = FindFirstEmptySlot();

                slots[slotIndex].color = Color.white;
                slots[slotIndex].sprite = abilityIcon.sprite;
            }
            else
            {
                foreach (var slot in slots)
                {
                    if (slot.sprite == abilityIcon.sprite)
                    {
                        slot.sprite = null;
                        slot.color = Color.clear;
                    }
                }
            }
        }

        /// <summary>
        /// Returns the first empty slot in the array
        /// </summary>
        private int FindFirstEmptySlot()
        {
            for (var i = 0; i < slots.Length; i++)
            {
                if (slots[i].sprite == null)
                {
                    return i;
                }
            }

            return 0;
        }

        /// <summary>
        /// Ensures the ability is not already active, as we shouldn't be able to call this method if so,
        /// else we add it to the active abilities
        /// </summary>
        public void ActivateAbility(Ability ability)
        {
            if (m_ActiveAbilities.Contains(ability))
            {
                throw new ArgumentException("Ability is already active.", nameof(ability));
            }

            m_ActiveAbilities.Add(ability);
        }

        /// <summary>
        /// Ensures we aren't trying to deactivate an ability that isn't active,
        /// as this shouldn't happen. Else, we deactivate it
        /// </summary>
        public void DeactivateAbility(Ability ability)
        {
            if (!m_ActiveAbilities.Contains(ability))
            {
                throw new ArgumentException("Ability is not active.", nameof(ability));
            }

            m_ActiveAbilities.Remove(ability);
        }

        /// <summary>
        /// Checks if the ability is currently active
        /// </summary>
        public bool IsAbilityActive(Ability ability)
        {
            return m_ActiveAbilities.Contains(ability);
        }
    }
}
