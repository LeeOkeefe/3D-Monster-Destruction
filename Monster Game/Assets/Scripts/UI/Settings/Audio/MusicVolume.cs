using UnityEngine;
using UnityEngine.UI;

namespace UI.Settings.Audio
{
    internal sealed class MusicVolume : MonoBehaviour
    {
        [SerializeField]
        private AudioSource audioSource;

        private Slider m_Slider;

        private void Start()
        {
            m_Slider = GetComponent<Slider>();
            audioSource.volume = m_Slider.value;
            m_Slider.onValueChanged.AddListener(delegate {ValueChangeCheck();});
        }

        /// <summary>
        /// Set our audioSource volume to the value of the slider
        /// </summary>
        public void ValueChangeCheck()
        {
            audioSource.volume = m_Slider.value;
        }
    }
}