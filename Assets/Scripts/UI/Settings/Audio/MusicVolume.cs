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
            m_Slider.value = PlayerPrefs.GetFloat("MusicVolume", 0.5F);
            m_Slider.onValueChanged.AddListener(delegate {ValueChangeCheck();});
        }

        /// <summary>
        /// Set our audioSource volume to the value of the slider
        /// </summary>
        public void ValueChangeCheck()
        {
            audioSource.volume = m_Slider.value;
            PlayerPrefs.SetFloat("MusicVolume", audioSource.volume);
            PlayerPrefs.Save();
        }
    }
}