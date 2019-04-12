using UnityEngine;
using UnityEngine.UI;

namespace UI.Settings
{
    internal sealed class MouseSensitivity : MonoBehaviour
    {
        public float Sensitivity { get; private set; } = 500;

        private Slider m_Slider;

        private void Start()
        {
            m_Slider = GetComponent<Slider>();
            m_Slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        }

        /// <summary>
        /// Set the sensitivity to the value of the slider
        /// </summary>
        public void ValueChangeCheck()
        {
            Sensitivity = m_Slider.value;
        }
    }
}
