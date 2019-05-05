using System.Collections;
using Abilities.Definitions;
using Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    internal sealed class AbilityDescription : MonoBehaviour
    {
        [SerializeField] private string abilityName;

        [TextArea(5, 5)] [SerializeField] private string abilityDescription;

        private float m_AbilityCost;
        private string m_AbilityCostText;

        [SerializeField] private Text descriptionText;
        [SerializeField] private CanvasGroup defaultText;
        [SerializeField] private CanvasGroup descriptionTextCanvasGroup;

        [SerializeField] private Font font;
        private const string Red = "<color=red>";
        private const string Green = "<color=green>";
        private const string Clear = "</color>";

        [SerializeField] private KeyCode keyCode;

        private void Start()
        {
            m_AbilityCost = GetComponent<Ability>().abilityCostInPoints;
            m_AbilityCostText = m_AbilityCost.ToString();
            descriptionTextCanvasGroup = descriptionText.GetComponent<CanvasGroup>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(keyCode))
            {
                defaultText.ToggleGroup(false);
                descriptionTextCanvasGroup.ToggleGroup(true);
                FormatText(ScoreManager.HasScore(m_AbilityCost) ? Green : Red);
            }
            else if (Input.GetKeyUp(keyCode))
            {
                descriptionTextCanvasGroup.ToggleGroup(false);
                defaultText.ToggleGroup(true);
            }
        }

        /// <summary>
        /// Display error message for one second
        /// </summary>
        /// <returns></returns>
        public IEnumerator InsufficientPoints()
        {
            var text = defaultText.GetComponent<Text>();
            text.font = font;
            text.text = $"{Red}{m_AbilityCost} points required for ability!{Clear}";

            yield return new WaitForSeconds(1);

            text.text = "F1 - F4 \n" + 
                         "Display Ability Information";
        }

        // Format text layout with text colours & properties
        //
        private void FormatText(string colour)
        {
            descriptionText.text = $"<b><color=blue> {abilityName} </color></b>\n " +
                                   $"<i><color=black> {abilityDescription} </color></i>\n" +
                                   $"<b>Points Required: {colour}{m_AbilityCostText} </color></b>";
        }
    }
}