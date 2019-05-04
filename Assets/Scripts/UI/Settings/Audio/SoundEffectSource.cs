using UnityEngine;

// Guarantee that we have an audio source on the gameobject
// this script is attached to, so we don't have to worry
// about handling null checks when calling GetComponent<T>
//
namespace UI.Settings.Audio
{
    [RequireComponent(typeof(AudioSource))]
    internal sealed class SoundEffectSource : MonoBehaviour
    {
        private AudioSource m_Source;

        // On awake, get our audio source and register it
        // with the global sound effect manager, so that
        // the audio source can be affected by volume changes
        // in the settings menu
        //
        private void Start()
        {
            m_Source = GetComponent<AudioSource>();
            SoundEffectManager.Instance.RegisterAudioSource(m_Source);
        }
    }
}