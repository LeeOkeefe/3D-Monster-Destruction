using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Settings.Audio
{
    internal sealed class SoundEffectManager : MonoBehaviour
    {
        private float m_Volume;
        public static SoundEffectManager Instance { get; private set; }

        private List<AudioSource> m_SoundEffects;

        private Slider m_Slider;

        private void Awake()
        {
            if (Instance != null)
                Destroy(this);

            Instance = this;

            m_Slider = GetComponent<Slider>();
            m_Slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
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
                if (soundEffect == null)
                {
                    m_SoundEffects.Remove(soundEffect);
                    continue;
                }

                soundEffect.volume = m_Volume;
            }
        }
    }
}
