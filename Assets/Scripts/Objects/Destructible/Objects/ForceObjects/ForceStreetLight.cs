using Objectives;
using Objects.Destructible.Definition;
using UI.Settings.Audio;
using UnityEngine;

namespace Objects.Destructible.Objects.ForceObjects
{
    internal sealed class ForceStreetLight : ForceObject
    {
        private bool m_Objective;

        [SerializeField]
        private AudioClip audioClip;

        // Use AddForce to push rigidBody over
        //
        private void OnTriggerEnter(Collider other)
        {
            if (!other.transform.root.CompareTag("Player"))
                return;

            AddForce(other, Rb);

            if (!m_Objective)
            {
                SoundEffectManager.Instance.PlayClipAtPoint(audioClip, transform.position);
                ObjectiveManager.Instance.ObjectiveProgressEvent(ObjectiveType.StreetLamp);
                m_Objective = true;
            }
        }
    }
}
