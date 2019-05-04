using UI.Settings.Audio;
using UnityEngine;

// Guarantee that we have an audio source on the gameobject
// this script is attached to, so we don't have to worry
// about handling null checks when calling GetComponent<T>
//
[RequireComponent(typeof(AudioSource))]
internal sealed class SoundEffectSource
{
    private AudioSource m_Source;

    // On awake, get our audio source and register it
    // with the global sound effect manager, so that
    // the audio source can be affected by volume changes
    // in the settings menu
    //
    private void Awake()
    {
        m_Source.GetComponent<AudioSource>();
        SoundEffects.Instance.RegisterAudioSource(m_Source);
    }
}