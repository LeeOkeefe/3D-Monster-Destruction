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

            if (Instance == null)
                Instance = this;

            m_Slider = GetComponent<Slider>();
            m_Slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
            m_SoundEffects = new List<AudioSource>();
            m_Slider.value = PlayerPrefs.GetFloat("SFXVolume", 0.5F);
            m_Volume = m_Slider.value;
        }

        /// <summary>
        /// Adds sound effect to the collection if it doesn't already exist
        /// </summary>
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
            PlayerPrefs.SetFloat("SFXVolume", m_Volume);
            PlayerPrefs.Save();

            for (var i = 0; i < m_SoundEffects.Count; i++)
            {
                if (m_SoundEffects[i] == null)
                {
                    m_SoundEffects.Remove(m_SoundEffects[i]);
                    continue;
                }

                m_SoundEffects[i].volume = m_Volume;
            }
        }

        /// <summary>
        /// Creates a temporary gameObject with an audioSource to play the passed in clip
        /// at the position, then destroys itself once the clip has finished
        /// </summary>
        public AudioSource PlayClipAtPoint(AudioClip clip, Vector3 pos)
        {
            var tempGameObject = new GameObject("TempAudio");
            tempGameObject.transform.position = pos;
            var audioSource = tempGameObject.AddComponent<AudioSource>();
            tempGameObject.AddComponent<SoundEffectSource>();
            audioSource.clip = clip;
            audioSource.Play();
            Destroy(tempGameObject, clip.length); 
            return audioSource; 
        }
    }
}
