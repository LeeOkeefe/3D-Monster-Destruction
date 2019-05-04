using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Settings.Audio
{
    internal sealed class SoundEffects : MonoBehaviour
    {
        private float m_Volume;
        public static SoundEffects Instance { get; private set; }

        private List<AudioSource> m_SoundEffects;

        private Slider m_Slider;

        private void Start()
        {
            if (Instance != null)
                Destroy(this);

            Instance = this;

            m_Slider = GetComponent<Slider>();
            m_SoundEffects = FindObjectsOfType<AudioSource>().ToList();
            m_Slider.onValueChanged.AddListener(delegate {ValueChangeCheck();});
            m_SoundEffects = new List<AudioSource>();
            m_Volume = m_Slider.value;
        }

        public void RegisterAudioSource(AudioSource audioSource)
        {
            if (m_SoundEffects.Contains(audioSource))
                return;

            audioSource.volume = m_Volume;
            m_SoundEffects.Add(audioSource);
        }

        /// <summary>
        /// Set the volume of each sound effect to the value of the slider
        /// </summary>
        public void ValueChangeCheck()
        {
            m_Volume = m_Slider.value;
            foreach (var soundEffect in m_SoundEffects)
            {
                soundEffect.volume = m_Volume;
            }
        }
    }
}
