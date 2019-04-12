using Abilities.Definitions;
using Extensions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

internal sealed class AbilityDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private string abilityName;

    [TextArea(5, 5)][SerializeField]
    private string abilityDescription;

    private string m_AbilityCost;

    [SerializeField]
    private CanvasGroup descriptionBackground;
    [SerializeField]
    private Text descriptionText;

    private void Start()
    {
        m_AbilityCost = GetComponent<Ability>().abilityCostInPoints.ToString();
    }

    // When cursor is over element, turn canvasGroup settings on & format text component
    //
    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionBackground.ToggleGroup(true);
        FormatText();
    }

    // When cursor exits element, turn canvasGroup settings off
    //
    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionBackground.ToggleGroup(false);
    }

    // Format text layout with text colours & properties
    //
    private void FormatText()
    {
        descriptionText.text = "<b><color=blue>" + abilityName + "</color></b>\n " +
                               "<i><color=black>" + abilityDescription + "</color></i>\n" +
                               "Points Required: <color=red>" + m_AbilityCost + "</color>";
    }
}
