using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Settings.Audio
{
    internal sealed class SoundEffects : MonoBehaviour
    {
        private AudioSource[] m_SoundEffects;

        private Slider m_Slider;

        private void Start()
        {
            m_Slider = GetComponent<Slider>();
            m_SoundEffects = FindObjectsOfType<AudioSource>();

            foreach (var soundEffect in m_SoundEffects)
            {
                soundEffect.volume = m_Slider.value;
            }

            m_Slider.onValueChanged.AddListener(delegate {ValueChangeCheck();});
        }

        /// <summary>
        /// Set the volume of each sound effect to the value of the slider
        /// </summary>
        public void ValueChangeCheck()
        {
            foreach (var soundEffect in m_SoundEffects)
            {
                soundEffect.volume = m_Slider.value;
            }
        }
    }
}
