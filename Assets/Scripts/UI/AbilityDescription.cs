using Abilities.Definitions;
using Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    internal sealed class AbilityDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField]
        private string abilityName;

        [TextArea(5, 5)][SerializeField]
        private string abilityDescription;

        private float m_AbilityCost;
        private string m_AbilityCostText;

        [SerializeField]
        private CanvasGroup descriptionBackground;
        [SerializeField]
        private Text descriptionText;

        private const string Red = "<color=red>";
        private const string Green = "<color=green>";

        private void Start()
        {
            m_AbilityCost = GetComponent<Ability>().abilityCostInPoints;
            m_AbilityCostText = m_AbilityCost.ToString();
        }

        // When cursor is over element, turn canvasGroup settings on & format text component
        //
        public void OnPointerEnter(PointerEventData eventData)
        {
            descriptionBackground.ToggleGroup(true);

            FormatText(ScoreManager.HasScore(m_AbilityCost) ? Green : Red);
        }

        // When cursor exits element, turn canvasGroup settings off
        //
        public void OnPointerExit(PointerEventData eventData)
        {
            descriptionBackground.ToggleGroup(false);
        }

        // Format text layout with text colours & properties
        //
        private void FormatText(string colour)
        {
            descriptionText.text = $"<b><color=blue> {abilityName} </color></b>\n " +
                                   $"<i><color=black> {abilityDescription} </color></i>\n" +
                                   $"<b>Points Required: {colour}{m_AbilityCostText} </color></b>";}
        }
    }
